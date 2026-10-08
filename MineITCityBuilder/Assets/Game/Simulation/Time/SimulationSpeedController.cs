using System;

namespace MineIT.CityBuilder.Simulation.Time
{
    public enum SimulationSpeed : byte
    {
        Paused = 0,
        Normal = 1,
        Fast = 2,
        VeryFast = 3,
        Laboratory = 4
    }

    /// <summary>
    /// Converts real elapsed milliseconds into a deterministic minute backlog.
    /// Partitioning the same elapsed time across different render frames produces
    /// the same total authoritative minute budget; maxSteps only controls catch-up rate.
    /// </summary>
    public sealed class SimulationSpeedController
    {
        private long _numeratorRemainder;
        private long _pendingMinutes;

        public SimulationSpeed Speed { get; set; } = SimulationSpeed.Normal;
        public long PendingMinutes => _pendingMinutes;

        public int AccumulateAndTake(long elapsedMilliseconds, int maxSteps)
        {
            if (elapsedMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(elapsedMilliseconds));
            }

            if (maxSteps < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxSteps));
            }

            var minutesPerSecond = GetMinutesPerSecond(Speed);
            if (minutesPerSecond == 0 || maxSteps == 0)
            {
                return 0;
            }

            if (Speed == SimulationSpeed.Laboratory)
            {
                return maxSteps;
            }

            var numerator = checked(elapsedMilliseconds * minutesPerSecond + _numeratorRemainder);
            _pendingMinutes = checked(_pendingMinutes + numerator / 1000L);
            _numeratorRemainder = numerator % 1000L;

            var steps = (int)Math.Min(_pendingMinutes, maxSteps);
            _pendingMinutes -= steps;
            return steps;
        }

        public void Reset()
        {
            _numeratorRemainder = 0;
            _pendingMinutes = 0;
        }

        public static int GetMinutesPerSecond(SimulationSpeed speed)
        {
            switch (speed)
            {
                case SimulationSpeed.Paused: return 0;
                case SimulationSpeed.Normal: return 5;
                case SimulationSpeed.Fast: return 20;
                case SimulationSpeed.VeryFast: return 80;
                case SimulationSpeed.Laboratory: return int.MaxValue;
                default: throw new ArgumentOutOfRangeException(nameof(speed), speed, null);
            }
        }
    }
}
