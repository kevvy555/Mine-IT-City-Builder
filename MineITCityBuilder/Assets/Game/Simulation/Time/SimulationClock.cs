using System;

namespace MineIT.CityBuilder.Simulation.Time
{
    public sealed class SimulationClock
    {
        public SimulationClock(long simulationMinute = 0)
        {
            if (simulationMinute < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(simulationMinute));
            }

            Minute = simulationMinute;
        }

        public long Minute { get; private set; }
        public bool IsPaused { get; private set; }

        public void SetPaused(bool paused) => IsPaused = paused;

        internal bool TryAdvanceOneMinute()
        {
            if (IsPaused)
            {
                return false;
            }

            Minute = checked(Minute + 1L);
            return true;
        }

        public void Restore(long minute, bool paused)
        {
            if (minute < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minute));
            }

            Minute = minute;
            IsPaused = paused;
        }
    }

    public readonly struct SimulationCadence
    {
        public SimulationCadence(int periodMinutes, int offsetMinutes = 0)
        {
            if (periodMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(periodMinutes));
            }

            if (offsetMinutes < 0 || offsetMinutes >= periodMinutes)
            {
                throw new ArgumentOutOfRangeException(nameof(offsetMinutes));
            }

            PeriodMinutes = periodMinutes;
            OffsetMinutes = offsetMinutes;
        }

        public int PeriodMinutes { get; }
        public int OffsetMinutes { get; }

        public bool RunsAt(long simulationMinute)
        {
            if (simulationMinute < OffsetMinutes)
            {
                return false;
            }

            return (simulationMinute - OffsetMinutes) % PeriodMinutes == 0;
        }

        public static SimulationCadence EveryMinute => new SimulationCadence(1);
        public static SimulationCadence EveryFiveMinutes => new SimulationCadence(5);
        public static SimulationCadence Hourly => new SimulationCadence(60);
        public static SimulationCadence Daily => new SimulationCadence(1440);
    }
}
