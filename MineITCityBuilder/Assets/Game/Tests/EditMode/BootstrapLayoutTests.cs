using System.IO;
using MineIT.CityBuilder.Bootstrap;
using NUnit.Framework;
using UnityEngine;

namespace MineIT.CityBuilder.Tests
{
    public sealed class BootstrapLayoutTests
    {
        [Test]
        public void Layout_IsDeterministicAndContainsTenThousandBuildings()
        {
            var first = BootstrapLayout.Generate(5300u);
            var second = BootstrapLayout.Generate(5300u);

            Assert.That(first, Has.Length.EqualTo(10_000));
            Assert.That(second, Has.Length.EqualTo(first.Length));

            for (var i = 0; i < first.Length; i += 137)
            {
                Assert.That(second[i].Position, Is.EqualTo(first[i].Position));
                Assert.That(second[i].Scale, Is.EqualTo(first[i].Scale));
                Assert.That(second[i].PaletteIndex, Is.EqualTo(first[i].PaletteIndex));
                Assert.That(second[i].Accent, Is.EqualTo(first[i].Accent));
            }
        }

        [Test]
        public void Layout_UsesOnlyKnownPalettesAndPositiveGeometry()
        {
            var buildings = BootstrapLayout.Generate(5300u);
            var accents = 0;

            foreach (var building in buildings)
            {
                Assert.That(building.Scale.x, Is.GreaterThan(0f));
                Assert.That(building.Scale.y, Is.GreaterThan(0f));
                Assert.That(building.Scale.z, Is.GreaterThan(0f));
                Assert.That(building.PaletteIndex, Is.InRange(0, 2));
                Assert.That(building.Position.y, Is.EqualTo(building.Scale.y * 0.5f).Within(0.0001f));

                if (building.Accent)
                {
                    accents++;
                }
            }

            Assert.That(accents, Is.GreaterThan(500));
            Assert.That(BootstrapLayout.CityCentre.x, Is.GreaterThan(250f));
            Assert.That(BootstrapLayout.CityExtent, Is.GreaterThan(500f));
        }

        [Test]
        public void PackageManifest_ContainsPinnedPhaseOneDependencies()
        {
            var projectRoot = Path.GetDirectoryName(Application.dataPath);
            var manifestPath = Path.Combine(projectRoot!, "Packages", "manifest.json");
            var manifest = File.ReadAllText(manifestPath);

            StringAssert.Contains("\"com.unity.entities\": \"1.5.0\"", manifest);
            StringAssert.Contains("\"com.unity.burst\": \"1.8.30\"", manifest);
            StringAssert.Contains("\"com.unity.inputsystem\": \"1.20.1\"", manifest);
            StringAssert.Contains("\"com.unity.render-pipelines.universal\": \"17.3.0\"", manifest);
        }
    }
}
