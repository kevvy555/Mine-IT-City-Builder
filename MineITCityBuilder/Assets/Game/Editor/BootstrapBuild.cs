using System;
using System.IO;
using MineIT.CityBuilder.Bootstrap;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MineIT.CityBuilder.Editor
{
    public static class BootstrapBuild
    {
        public const string ApplicationIdentifier = "com.mineit.citybuilder";
        public const string GeneratedScenePath = "Assets/Game/Generated/Bootstrap/Bootstrap.unity";
        public const string PipelineAssetPath = "Assets/Game/Generated/Settings/MineIT_URP.asset";
        public const string BuildInfoPath = "Assets/Game/Generated/Resources/build-info.json";
        public const string GeneratedResourcesRoot = "Assets/Game/Generated/Resources";
        public const string CanonProvenancePath = "Assets/Game/Generated/Resources/Canon/canon.provenance.json";
        public const string DevSigningConfigRelativePath = "config/android-dev-signing.json";
        public const string CiBuildContextRelativePath = "MineITCityBuilder/Library/MineIT/ci-build-context.json";

        [Serializable]
        private sealed class CanonBuildProvenance
        {
            public string universeCommit;
            public string contentHashSha256;
        }

        [Serializable]
        private sealed class DevSigningConfig
        {
            public string purpose;
            public string alias;
            public string storePassword;
            public string keyPassword;
            public string sha256Fingerprint;
            public string keystoreBase64;
        }

        [Serializable]
        private sealed class CiBuildContext
        {
            public string runNumber;
            public string gitSha;
        }

        [MenuItem("MineIT/Bootstrap/Configure Android Project")]
        public static void ConfigureProject()
        {
            PlayerSettings.companyName = "MineIT";
            PlayerSettings.productName = "MineIT City Builder";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel29;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel36;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.buildApkPerCpuArchitecture = false;

            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;

            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
            PlayerSettings.SetGraphicsAPIs(
                BuildTarget.Android,
                new[] { GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3 });

            EditorUserBuildSettings.buildAppBundle = false;
            CreateOrAssignUrp();
            CreatePrimitiveMaterials();
            WriteBuildInfo();
            CreateBootstrapScene();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log(
                $"MineIT Android configured: min API 29, target API 36, ARM64/IL2CPP, " +
                $"Vulkan -> OpenGLES3 fallback, Unity {Application.unityVersion}.");
        }

        public static void BuildAndroid()
        {
            ConfigureProject();

            if (!EditorUserBuildSettings.SwitchActiveBuildTarget(
                    BuildTargetGroup.Android,
                    BuildTarget.Android))
            {
                throw new BuildFailedException("Could not switch to Android build target.");
            }

            var buildContext = ResolveBuildContext();
            var runNumber = buildContext.runNumber;
            PlayerSettings.bundleVersion = runNumber == "local"
                ? "0.1.local"
                : $"0.1.{runNumber}";

            if (!int.TryParse(runNumber, out var versionCode) || versionCode <= 0)
            {
                versionCode = 1;
            }

            PlayerSettings.Android.bundleVersionCode = versionCode;

            var repositoryRoot = RepositoryRoot();
            ConfigureDevelopmentSigning(repositoryRoot);

            var outputDirectory = Path.Combine(repositoryRoot, "build", "Android");
            Directory.CreateDirectory(outputDirectory);

            var shortSha = string.IsNullOrWhiteSpace(buildContext.gitSha)
                ? "local"
                : buildContext.gitSha.Substring(0, Math.Min(8, buildContext.gitSha.Length));
            var outputPath = Path.Combine(
                outputDirectory,
                $"MineIT-City-Builder-{PlayerSettings.bundleVersion}-{shortSha}-dev.apk");

            var options = new BuildPlayerOptions
            {
                scenes = new[] { GeneratedScenePath },
                locationPathName = outputPath,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.Development
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new BuildFailedException(
                    $"Android build failed: {report.summary.result}, " +
                    $"{report.summary.totalErrors} errors.");
            }

            Debug.Log(
                $"MineIT Android APK built: {outputPath} " +
                $"({report.summary.totalSize:N0} bytes in {report.summary.totalTime}).");
        }

        private static void CreateOrAssignUrp()
        {
            EnsureDirectory(Path.GetDirectoryName(PipelineAssetPath));

            var pipeline = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(PipelineAssetPath);
            if (pipeline == null)
            {
                pipeline = UniversalRenderPipelineAsset.Create();
                pipeline.name = "MineIT Mobile URP";
                AssetDatabase.CreateAsset(pipeline, PipelineAssetPath);
                pipeline.LoadBuiltinRendererData();
            }

            GraphicsSettings.defaultRenderPipeline = pipeline;
            QualitySettings.renderPipeline = pipeline;
            EditorUtility.SetDirty(pipeline);
        }

        private static void CreatePrimitiveMaterials()
        {
            var shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
            {
                throw new BuildFailedException(
                    "URP Lit shader is unavailable while generating bootstrap PVG materials.");
            }

            foreach (var definition in PrimitiveMaterialLibrary.Definitions)
            {
                var assetPath = $"{GeneratedResourcesRoot}/{definition.ResourcePath}.mat";
                EnsureDirectory(Path.GetDirectoryName(assetPath));

                var material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
                if (material == null)
                {
                    material = new Material(shader);
                    AssetDatabase.CreateAsset(material, assetPath);
                }
                else
                {
                    material.shader = shader;
                }

                material.name = definition.DisplayName;
                material.enableInstancing = true;

                if (material.HasProperty("_BaseColor"))
                {
                    material.SetColor("_BaseColor", definition.Colour);
                }
                else
                {
                    material.color = definition.Colour;
                }

                if (material.HasProperty("_Smoothness"))
                {
                    material.SetFloat("_Smoothness", 0.42f);
                }

                EditorUtility.SetDirty(material);
            }

            AssetDatabase.SaveAssets();
        }

        private static void CreateBootstrapScene()
        {
            EnsureDirectory(Path.GetDirectoryName(GeneratedScenePath));

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var root = new GameObject("MineIT City Builder Bootstrap");
            root.AddComponent<BootstrapApp>();

            if (!EditorSceneManager.SaveScene(scene, GeneratedScenePath))
            {
                throw new BuildFailedException($"Could not save bootstrap scene: {GeneratedScenePath}");
            }

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(GeneratedScenePath, true)
            };
        }

        private static void WriteBuildInfo()
        {
            EnsureDirectory(Path.GetDirectoryName(BuildInfoPath));

            if (!File.Exists(CanonProvenancePath))
            {
                throw new BuildFailedException(
                    $"Locked canon provenance is missing: {CanonProvenancePath}. Run the Universe importer first.");
            }

            var provenance = JsonUtility.FromJson<CanonBuildProvenance>(
                File.ReadAllText(CanonProvenancePath));
            if (provenance == null ||
                string.IsNullOrWhiteSpace(provenance.universeCommit) ||
                string.IsNullOrWhiteSpace(provenance.contentHashSha256))
            {
                throw new BuildFailedException("Locked canon provenance is invalid.");
            }

            var buildContext = ResolveBuildContext();
            var info = new BuildInfoData
            {
                version = buildContext.runNumber == "local" ? "0.1.local" : $"0.1.{buildContext.runNumber}",
                gitSha = buildContext.gitSha,
                runNumber = buildContext.runNumber,
                unityVersion = Application.unityVersion,
                universeCommit = provenance.universeCommit,
                canonContentHash = provenance.contentHashSha256,
                createdUtc = DateTime.UtcNow.ToString("O")
            };

            File.WriteAllText(BuildInfoPath, JsonUtility.ToJson(info, true));
        }

        private static void ConfigureDevelopmentSigning(string repositoryRoot)
        {
            var configPath = Path.Combine(repositoryRoot, DevSigningConfigRelativePath);
            if (!File.Exists(configPath))
            {
                throw new BuildFailedException(
                    $"Development signing config is missing: {configPath}");
            }

            var config = JsonUtility.FromJson<DevSigningConfig>(File.ReadAllText(configPath));
            if (config == null ||
                config.purpose != "development-only" ||
                string.IsNullOrWhiteSpace(config.alias) ||
                string.IsNullOrWhiteSpace(config.storePassword) ||
                string.IsNullOrWhiteSpace(config.keyPassword) ||
                string.IsNullOrWhiteSpace(config.keystoreBase64))
            {
                throw new BuildFailedException(
                    "Development signing config is invalid or is not explicitly development-only.");
            }

            byte[] keystoreBytes;
            try
            {
                keystoreBytes = Convert.FromBase64String(config.keystoreBase64);
            }
            catch (FormatException exception)
            {
                throw new BuildFailedException(
                    $"Development signing keystore is not valid base64: {exception.Message}");
            }

            var keystorePath = Path.Combine(
                repositoryRoot,
                "MineITCityBuilder",
                "Library",
                "MineIT",
                "mineit-ci-dev.keystore");
            EnsureDirectory(Path.GetDirectoryName(keystorePath));
            File.WriteAllBytes(keystorePath, keystoreBytes);

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = keystorePath;
            PlayerSettings.Android.keystorePass = config.storePassword;
            PlayerSettings.Android.keyaliasName = config.alias;
            PlayerSettings.Android.keyaliasPass = config.keyPassword;

            Debug.Log(
                $"MineIT development APK signing enabled: alias={config.alias}, " +
                $"SHA-256={config.sha256Fingerprint}. This key is development-only.");
        }

        private static CiBuildContext ResolveBuildContext()
        {
            var environmentRun = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER");
            var environmentSha = Environment.GetEnvironmentVariable("GITHUB_SHA");
            if (!string.IsNullOrWhiteSpace(environmentRun) &&
                !string.IsNullOrWhiteSpace(environmentSha))
            {
                return new CiBuildContext
                {
                    runNumber = environmentRun,
                    gitSha = environmentSha
                };
            }

            var contextPath = Path.Combine(RepositoryRoot(), CiBuildContextRelativePath);
            if (File.Exists(contextPath))
            {
                var context = JsonUtility.FromJson<CiBuildContext>(File.ReadAllText(contextPath));
                if (context != null &&
                    !string.IsNullOrWhiteSpace(context.runNumber) &&
                    !string.IsNullOrWhiteSpace(context.gitSha))
                {
                    return context;
                }
            }

            return new CiBuildContext
            {
                runNumber = "local",
                gitSha = "local"
            };
        }

        private static string RepositoryRoot()
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
        }

        private static void EnsureDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }
    }
}
