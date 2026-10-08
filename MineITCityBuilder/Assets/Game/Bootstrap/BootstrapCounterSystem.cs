using Unity.Burst;
using Unity.Entities;

namespace MineIT.CityBuilder.Bootstrap
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct BootstrapCounterSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, new BootstrapCounter());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var counter = SystemAPI.GetSingletonRW<BootstrapCounter>();
            counter.ValueRW.Ticks++;
        }
    }
}
