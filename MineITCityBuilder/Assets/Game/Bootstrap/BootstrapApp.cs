using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MineIT.CityBuilder.Bootstrap
{
    public sealed class BootstrapApp : MonoBehaviour
    {
        private World _world;
        private EntityManager _entityManager;
        private EntityQuery _agentQuery;
        private EntityQuery _counterQuery;
        private PrimitiveCityRenderer _renderer;
        private TouchOrbitCamera _cameraController;
        private BootstrapUi _ui;
        private bool _paused;
        private bool _focused = true;
        private float _nextMetricsUpdate;
        private bool _queriesReady;

        private void Awake()
        {
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;

            CreatePresentation();
            CreateBootstrapEntities();
        }

        private void CreatePresentation()
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.38f, 0.43f, 0.46f);

            var city = new GameObject("PVG City Renderer");
            _renderer = city.AddComponent<PrimitiveCityRenderer>();

            var cameraObject = new GameObject("City Camera");
            var camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.055f, 0.085f, 0.105f);
            camera.nearClipPlane = 0.5f;
            camera.farClipPlane = 3000f;
            camera.fieldOfView = 42f;
            cameraObject.AddComponent<AudioListener>();
            _cameraController = cameraObject.AddComponent<TouchOrbitCamera>();

            var lightObject = new GameObject("Commonwealth Sun");
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(1f, 0.96f, 0.88f);
            light.intensity = 1.15f;
            light.shadows = LightShadows.Soft;
            lightObject.transform.rotation = Quaternion.Euler(48f, -32f, 0f);

            var uiObject = new GameObject("Bootstrap UI");
            _ui = uiObject.AddComponent<BootstrapUi>();
            _ui.Initialise(() => _cameraController.ResetView());
        }

        private void CreateBootstrapEntities()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null || !world.IsCreated)
            {
                Debug.LogError("DOTS default world was not created.");
                return;
            }

            _world = world;
            _entityManager = world.EntityManager;
            var archetype = _entityManager.CreateArchetype(typeof(BootstrapAgent));
            using var entities = new NativeArray<Entity>(BootstrapLayout.BuildingCount, Allocator.Temp);
            _entityManager.CreateEntity(archetype, entities);

            for (var i = 0; i < entities.Length; i++)
            {
                _entityManager.SetComponentData(entities[i], new BootstrapAgent { Index = i });
            }

            _agentQuery = _entityManager.CreateEntityQuery(ComponentType.ReadOnly<BootstrapAgent>());
            _counterQuery = _entityManager.CreateEntityQuery(ComponentType.ReadOnly<BootstrapCounter>());
            _queriesReady = true;
        }

        private void Update()
        {
            if (Time.unscaledTime < _nextMetricsUpdate)
            {
                return;
            }

            _nextMetricsUpdate = Time.unscaledTime + 0.25f;
            var agents = _queriesReady ? _agentQuery.CalculateEntityCount() : 0;
            var ticks = _queriesReady && _counterQuery.CalculateEntityCount() == 1
                ? _counterQuery.GetSingleton<BootstrapCounter>().Ticks
                : 0u;
            var instances = _renderer != null ? _renderer.TotalInstanceCount : 0;
            _ui.SetMetrics(agents, ticks, instances);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            _paused = pauseStatus;
            _ui?.SetLifecycle(_paused, _focused);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _focused = hasFocus;
            _ui?.SetLifecycle(_paused, _focused);
        }

        private void OnDestroy()
        {
            if (_queriesReady && _world != null && _world.IsCreated)
            {
                _entityManager.DestroyEntity(_agentQuery);
            }

            _queriesReady = false;
        }
    }
}
