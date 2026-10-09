using System.Linq;
using MineIT.CityBuilder.Bootstrap;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

namespace MineIT.CityBuilder.Tests
{
    public sealed class BootstrapUiTests
    {
        [Test]
        public void BootstrapUi_UsesPackagedRuntimeThemeAndBuildsTextControls()
        {
            var gameObject = new GameObject("Bootstrap UI Test");

            try
            {
                var ui = gameObject.AddComponent<BootstrapUi>();
                ui.Initialise(null);

                var document = gameObject.GetComponent<UIDocument>();
                Assert.That(document, Is.Not.Null);
                Assert.That(document.panelSettings, Is.Not.Null);
                Assert.That(
                    document.panelSettings.themeStyleSheet,
                    Is.Not.Null,
                    "Runtime-created PanelSettings must reference a packaged theme so fonts and built-in controls render in players.");

                var labels = document.rootVisualElement.Query<Label>().ToList();
                Assert.That(labels.Count, Is.GreaterThanOrEqualTo(7));
                Assert.That(
                    labels.Any(label => label.text == "MINEIT // CONCORDIA BOOTSTRAP"),
                    Is.True);

                var buttons = document.rootVisualElement.Query<Button>().ToList();
                Assert.That(buttons.Count, Is.EqualTo(1));
                Assert.That(buttons[0].text, Is.EqualTo("RESET CITY VIEW"));

                ui.SetCanonStatus(
                    "Koplin 3",
                    "Concordia",
                    "Federal Forum",
                    "(0,0)",
                    16,
                    100,
                    50,
                    50,
                    "2a3251ba",
                    "64d2c982");

                Assert.That(
                    labels.Any(label => label.text.Contains("CANON ✓ Koplin 3 / Concordia / Federal Forum (0,0)")),
                    Is.True);
            }
            finally
            {
                Object.DestroyImmediate(gameObject);
            }
        }

        [Test]
        public void BootstrapRuntimeTheme_ImportsUnityDefaultRuntimeTheme()
        {
            var theme = Resources.Load<ThemeStyleSheet>("bootstrap-runtime-theme");
            Assert.That(theme, Is.Not.Null);
        }
    }
}
