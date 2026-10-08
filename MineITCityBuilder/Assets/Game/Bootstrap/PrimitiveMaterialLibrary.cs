using UnityEngine;

namespace MineIT.CityBuilder.Bootstrap
{
    public readonly struct PrimitiveMaterialDefinition
    {
        public PrimitiveMaterialDefinition(string resourcePath, string displayName, Color colour)
        {
            ResourcePath = resourcePath;
            DisplayName = displayName;
            Colour = colour;
        }

        public string ResourcePath { get; }
        public string DisplayName { get; }
        public Color Colour { get; }
    }

    public static class PrimitiveMaterialLibrary
    {
        public const string PaleStructure = "PVG/pale-structure";
        public const string BlueGlass = "PVG/blue-glass";
        public const string LightComposite = "PVG/light-composite";
        public const string CommonwealthOrange = "PVG/commonwealth-orange";
        public const string Ground = "PVG/ground";

        public static readonly PrimitiveMaterialDefinition[] Definitions =
        {
            new PrimitiveMaterialDefinition(PaleStructure, "PVG Pale Structure", new Color(0.82f, 0.86f, 0.88f)),
            new PrimitiveMaterialDefinition(BlueGlass, "PVG Blue Glass", new Color(0.26f, 0.48f, 0.62f)),
            new PrimitiveMaterialDefinition(LightComposite, "PVG Light Composite", new Color(0.64f, 0.69f, 0.71f)),
            new PrimitiveMaterialDefinition(CommonwealthOrange, "PVG Commonwealth Orange", new Color(0.93f, 0.38f, 0.08f)),
            new PrimitiveMaterialDefinition(Ground, "PVG Ground", new Color(0.055f, 0.071f, 0.081f))
        };
    }
}
