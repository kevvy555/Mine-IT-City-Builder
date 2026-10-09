using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MineIT.CityBuilder.Bootstrap
{
    public sealed class BootstrapUi : MonoBehaviour
    {
        private Label _build;
        private Label _canon;
        private Label _metrics;
        private Label _lifecycle;
        private Label _fps;
        private float _smoothedDelta = 1f / 30f;

        public void Initialise(Action resetCamera)
        {
            var document = gameObject.AddComponent<UIDocument>();
            var panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
            panelSettings.name = "MineIT Bootstrap Panel";
            panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
            panelSettings.referenceResolution = new Vector2Int(1920, 1080);
            panelSettings.screenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;
            panelSettings.match = 0.5f;
            document.panelSettings = panelSettings;

            var root = document.rootVisualElement;
            root.style.position = Position.Absolute;
            root.style.left = 0;
            root.style.top = 0;
            root.style.right = 0;
            root.style.bottom = 0;

            var card = new VisualElement();
            card.style.position = Position.Absolute;
            card.style.left = 22;
            card.style.top = 22;
            card.style.width = 610;
            card.style.paddingLeft = 18;
            card.style.paddingRight = 18;
            card.style.paddingTop = 14;
            card.style.paddingBottom = 14;
            card.style.backgroundColor = new Color(0.025f, 0.035f, 0.045f, 0.92f);
            card.style.borderTopLeftRadius = 12;
            card.style.borderTopRightRadius = 12;
            card.style.borderBottomLeftRadius = 12;
            card.style.borderBottomRightRadius = 12;
            root.Add(card);

            var title = new Label("MINEIT // CONCORDIA BOOTSTRAP");
            title.style.fontSize = 24;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.color = new Color(0.95f, 0.43f, 0.10f);
            title.style.marginBottom = 8;
            card.Add(title);

            _build = CreateLabel(card, 16, new Color(0.78f, 0.86f, 0.90f));
            _canon = CreateLabel(card, 15, new Color(0.70f, 0.90f, 0.76f));
            _metrics = CreateLabel(card, 16, Color.white);
            _lifecycle = CreateLabel(card, 15, new Color(0.60f, 0.76f, 0.84f));
            _fps = CreateLabel(card, 15, new Color(0.60f, 0.76f, 0.84f));

            var controls = CreateLabel(card, 14, new Color(0.72f, 0.74f, 0.75f));
            controls.text = "Touch: drag to pan • pinch to zoom • two-finger twist to rotate";

            var reset = new Button(() => resetCamera?.Invoke()) { text = "RESET CITY VIEW" };
            reset.style.height = 48;
            reset.style.marginTop = 10;
            reset.style.fontSize = 15;
            card.Add(reset);

            var info = BuildInfo.Current;
            var shortSha = string.IsNullOrWhiteSpace(info.gitSha)
                ? "unknown"
                : info.gitSha.Substring(0, Mathf.Min(8, info.gitSha.Length));
            _build.text = $"v{info.version} • Unity {Application.unityVersion} • {shortSha} • run {info.runNumber}";
            _canon.text = "Canon: loading locked Universe snapshot…";
            _lifecycle.text = "Application: active";
        }

        public void SetCanonStatus(
            string planet,
            string settlement,
            string district,
            string coordinate,
            int chunkCount,
            int tileCount,
            int generatedImages,
            int pendingImages,
            string universeSha,
            string contentHash)
        {
            _canon.text =
                $"CANON ✓ {planet} / {settlement} / {district} {coordinate}\n" +
                $"Atlas: {tileCount} tiles • origin chunks: {chunkCount} • art {generatedImages}/{tileCount}\n" +
                $"Universe {universeSha} • canon hash {contentHash}";
        }

        public void SetCanonError(string message)
        {
            _canon.style.color = new Color(1f, 0.42f, 0.32f);
            _canon.text = $"CANON ERROR: {message}";
        }

        public void SetMetrics(int ecsAgents, uint simulationTicks, int renderInstances)
        {
            _metrics.text = $"ECS agents: {ecsAgents:N0}  •  Burst ticks: {simulationTicks:N0}\nPVG instances: {renderInstances:N0}";
        }

        public void SetLifecycle(bool paused, bool focused)
        {
            _lifecycle.text = paused
                ? "Application: paused/background"
                : focused
                    ? "Application: active"
                    : "Application: focus lost";
        }

        private void Update()
        {
            _smoothedDelta = Mathf.Lerp(_smoothedDelta, Time.unscaledDeltaTime, 0.08f);
            var fps = _smoothedDelta > 0.0001f ? 1f / _smoothedDelta : 0f;
            _fps.text = $"Render: {fps:0} FPS target {Application.targetFrameRate}";
        }

        private static Label CreateLabel(VisualElement parent, int size, Color colour)
        {
            var label = new Label();
            label.style.fontSize = size;
            label.style.color = colour;
            label.style.marginBottom = 5;
            parent.Add(label);
            return label;
        }
    }
}
