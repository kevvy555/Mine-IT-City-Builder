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
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

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

            var runNumber = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER") ?? "0";
            PlayerSettings.bundleVersion = $"0.1.{runNumber}";

            if (!int.TryParse(runNumber, out var versionCode) || versionCode <= 0)
            {
                versionCode = 1;
            }

            PlayerSettings.Android.bundleVersionCode = versionCode;

            var repositoryRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
            var outputDirectory = Path.Combine(repositoryRoot, "build", "Android");
            Directory.CreateDirectory(outputDirectory);

            var shortSha = Environment.GetEnvironmentVariable("GITHUB_SHA");
            shortSha = string.IsNullOrWhiteSpace(shortSha)
                ? "local"
                : shortSha.Substring(0, Math.Min(8, shortSha.Length));
            var outputPath = Path.Combine(
                outputDirectory,
                $"MineIT-City-Builder-0.1.{runNumber}-{shortSha}-dev.apk");

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

            var runNumber = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER") ?? "local";
            var sha = Environment.GetEnvironmentVariable("GITHUB_SHA") ?? "local";
            var info = new BuildInfoData
            {
                version = runNumber == "local" ? "0.1.local" : $"0.1.{runNumber}",
                gitSha = sha,
                runNumber = runNumber,
                unityVersion = Application.unityVersion,
                createdUtc = DateTime.UtcNow.ToString("O")
            };

            File.WriteAllText(BuildInfoPath, JsonUtility.ToJson(info, true));
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
