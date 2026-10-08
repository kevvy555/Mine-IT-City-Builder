using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MineIT.CityBuilder.Bootstrap
{
    public sealed class PrimitiveCityRenderer : MonoBehaviour
    {
        private const int MaxBatch = 1023;

        private readonly List<Matrix4x4>[] _buildings =
        {
            new List<Matrix4x4>(4000),
            new List<Matrix4x4>(4000),
            new List<Matrix4x4>(4000)
        };

        private readonly List<Matrix4x4> _accents = new List<Matrix4x4>(1600);
        private readonly Matrix4x4[] _batch = new Matrix4x4[MaxBatch];

        private Mesh _cube;
        private Material[] _buildingMaterials;
        private Material _accentMaterial;
        private Material _groundMaterial;
        private Matrix4x4 _ground;
        private bool _materialsReady;

        public int BaseInstanceCount { get; private set; }
        public int AccentInstanceCount => _accents.Count;
        public int TotalInstanceCount => BaseInstanceCount + AccentInstanceCount + 1;

        private void OnEnable()
        {
            _cube = PrimitiveMeshFactory.CreateUnitCube();

            _buildingMaterials = new[]
            {
                LoadMaterial(PrimitiveMaterialLibrary.PaleStructure),
                LoadMaterial(PrimitiveMaterialLibrary.BlueGlass),
                LoadMaterial(PrimitiveMaterialLibrary.LightComposite)
            };
            _accentMaterial = LoadMaterial(PrimitiveMaterialLibrary.CommonwealthOrange);
            _groundMaterial = LoadMaterial(PrimitiveMaterialLibrary.Ground);

            _materialsReady =
                _buildingMaterials[0] != null &&
                _buildingMaterials[1] != null &&
                _buildingMaterials[2] != null &&
                _accentMaterial != null &&
                _groundMaterial != null;

            if (!_materialsReady)
            {
                Debug.LogError(
                    "MineIT bootstrap PVG materials are missing from the player build. " +
                    "The renderer has been disabled to avoid repeated draw exceptions.");
                enabled = false;
                return;
            }

            var instances = BootstrapLayout.Generate();
            BaseInstanceCount = instances.Length;

            foreach (var instance in instances)
            {
                var matrix = Matrix4x4.TRS(instance.Position, Quaternion.identity, instance.Scale);
                _buildings[instance.PaletteIndex].Add(matrix);

                if (!instance.Accent)
                {
                    continue;
                }

                var accentScale = new Vector3(
                    Mathf.Max(0.18f, instance.Scale.x * 0.10f),
                    instance.Scale.y * 0.72f,
                    instance.Scale.z + 0.04f);
                var accentPosition = instance.Position + new Vector3(
                    instance.Scale.x * 0.51f,
                    instance.Scale.y * 0.06f,
                    0f);
                _accents.Add(Matrix4x4.TRS(accentPosition, Quaternion.identity, accentScale));
            }

            var extent = BootstrapLayout.CityExtent + 70f;
            var centre = BootstrapLayout.CityCentre + new Vector3(0f, -0.55f, 0f);
            _ground = Matrix4x4.TRS(centre, Quaternion.identity, new Vector3(extent, 1f, extent));
        }

        private void LateUpdate()
        {
            if (_cube == null || !_materialsReady)
            {
                return;
            }

            _batch[0] = _ground;
            Graphics.DrawMeshInstanced(
                _cube, 0, _groundMaterial, _batch, 1, null,
                ShadowCastingMode.Off, true, gameObject.layer, null, LightProbeUsage.Off);

            for (var i = 0; i < _buildings.Length; i++)
            {
                DrawBatches(_buildings[i], _buildingMaterials[i]);
            }

            DrawBatches(_accents, _accentMaterial);
        }

        private void DrawBatches(List<Matrix4x4> matrices, Material material)
        {
            if (material == null)
            {
                return;
            }

            for (var start = 0; start < matrices.Count; start += MaxBatch)
            {
                var count = Mathf.Min(MaxBatch, matrices.Count - start);
                matrices.CopyTo(start, _batch, 0, count);
                Graphics.DrawMeshInstanced(
                    _cube, 0, material, _batch, count, null,
                    ShadowCastingMode.Off, true, gameObject.layer, null, LightProbeUsage.Off);
            }
        }

        private static Material LoadMaterial(string resourcePath)
        {
            var material = Resources.Load<Material>(resourcePath);
            if (material == null)
            {
                Debug.LogError($"Missing packaged PVG material: Resources/{resourcePath}.mat");
            }

            return material;
        }

        private void OnDisable()
        {
            if (_cube != null)
            {
                Destroy(_cube);
                _cube = null;
            }
        }
    }
}
