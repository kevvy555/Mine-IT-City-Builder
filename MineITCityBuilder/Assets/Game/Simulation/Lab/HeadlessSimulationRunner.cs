using System;
using MineIT.CityBuilder.Simulation.Scheduling;

namespace MineIT.CityBuilder.Simulation.Lab
{
    public sealed class HeadlessSimulationRunner
    {
        public HeadlessSimulationRunner(DeterministicScheduler scheduler)
        {
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public DeterministicScheduler Scheduler { get; }

        public ulong RunMinutes(long minutes)
        {
            if (minutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes));
            }

            for (long i = 0; i < minutes; i++)
            {
                if (!Scheduler.AdvanceOneMinute())
                {
                    break;
                }
            }

            return Scheduler.ComputeChecksum();
        }

        public ulong[] RunWithCheckpoints(long minutes, int checkpointEveryMinutes)
        {
            if (minutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes));
            }

            if (checkpointEveryMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(checkpointEveryMinutes));
            }

            var count = (int)((minutes + checkpointEveryMinutes - 1L) / checkpointEveryMinutes);
            var checksums = new ulong[count];
            var index = 0;

            for (long elapsed = 0; elapsed < minutes;)
            {
                var batch = Math.Min((long)checkpointEveryMinutes, minutes - elapsed);
                RunMinutes(batch);
                checksums[index++] = Scheduler.ComputeChecksum();
                elapsed += batch;
            }

            return checksums;
        }
    }
}
