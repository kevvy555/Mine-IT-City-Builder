using Unity.Entities;

namespace MineIT.CityBuilder.Bootstrap
{
    public struct BootstrapAgent : IComponentData
    {
        public int Index;
    }

    public struct BootstrapCounter : IComponentData
    {
        public uint Ticks;
    }
}
