using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace MineIT.CityBuilder.Tests
{
    public sealed class ArchitectureBoundaryTests
    {
        [Test]
        public void SimulationCoreAndPersistence_DoNotReferencePresentationAssemblies()
        {
            var root = Path.GetDirectoryName(Application.dataPath);
            Assert.That(root, Is.Not.Null);

            var core = File.ReadAllText(Path.Combine(
                root!, "Assets", "Game", "Core", "MineIT.CityBuilder.Core.asmdef"));
            var simulation = File.ReadAllText(Path.Combine(
                root!, "Assets", "Game", "Simulation", "MineIT.CityBuilder.Simulation.asmdef"));
            var persistence = File.ReadAllText(Path.Combine(
                root!, "Assets", "Game", "Persistence", "MineIT.CityBuilder.Persistence.asmdef"));

            StringAssert.DoesNotContain("MineIT.CityBuilder.Bootstrap", core);
            StringAssert.DoesNotContain("MineIT.CityBuilder.Bootstrap", simulation);
            StringAssert.DoesNotContain("MineIT.CityBuilder.Bootstrap", persistence);

            StringAssert.Contains("noEngineReferences", core);
            StringAssert.Contains("noEngineReferences", simulation);
            StringAssert.Contains("noEngineReferences", persistence);

            StringAssert.Contains("MineIT.CityBuilder.Core", simulation);
            StringAssert.DoesNotContain("MineIT.CityBuilder.Persistence", simulation);

            StringAssert.Contains("MineIT.CityBuilder.Core", persistence);
            StringAssert.Contains("MineIT.CityBuilder.Simulation", persistence);
        }
    }
}
