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

        public int BaseInstanceCount { get; private set; }
        public int AccentInstanceCount => _accents.Count;
        public int TotalInstanceCount => BaseInstanceCount + AccentInstanceCount + 1;

        private void OnEnable()
        {
            _cube = PrimitiveMeshFactory.CreateUnitCube();
            _buildingMaterials = new[]
            {
                CreateMaterial("PVG Pale Structure", new Color(0.82f, 0.86f, 0.88f)),
                CreateMaterial("PVG Blue Glass", new Color(0.26f, 0.48f, 0.62f)),
                CreateMaterial("PVG Light Composite", new Color(0.64f, 0.69f, 0.71f))
            };
            _accentMaterial = CreateMaterial("PVG Commonwealth Orange", new Color(0.93f, 0.38f, 0.08f));
            _groundMaterial = CreateMaterial("PVG Ground", new Color(0.055f, 0.071f, 0.081f));

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
            if (_cube == null)
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
            for (var start = 0; start < matrices.Count; start += MaxBatch)
            {
                var count = Mathf.Min(MaxBatch, matrices.Count - start);
                matrices.CopyTo(start, _batch, 0, count);
                Graphics.DrawMeshInstanced(
                    _cube, 0, material, _batch, count, null,
                    ShadowCastingMode.Off, true, gameObject.layer, null, LightProbeUsage.Off);
            }
        }

        private static Material CreateMaterial(string name, Color colour)
        {
            var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            var material = new Material(shader)
            {
                name = name,
                enableInstancing = true
            };

            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", colour);
            }
            else
            {
                material.color = colour;
            }

            if (material.HasProperty("_Smoothness"))
            {
                material.SetFloat("_Smoothness", 0.42f);
            }

            return material;
        }

        private void OnDisable()
        {
            if (_cube != null)
            {
                Destroy(_cube);
            }

            if (_buildingMaterials != null)
            {
                foreach (var material in _buildingMaterials)
                {
                    if (material != null)
                    {
                        Destroy(material);
                    }
                }
            }

            if (_accentMaterial != null)
            {
                Destroy(_accentMaterial);
            }

            if (_groundMaterial != null)
            {
                Destroy(_groundMaterial);
            }
        }
    }
}
