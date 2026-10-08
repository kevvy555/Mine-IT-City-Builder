using System;
using MineIT.CityBuilder.Core.Ids;
using Unity.Collections;

namespace MineIT.CityBuilder.Core.Diagnostics
{
    public readonly struct DiagnosticId : IEquatable<DiagnosticId>
    {
        private readonly FixedString64Bytes _value;

        public DiagnosticId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Diagnostic ID is required.", nameof(value));
            }

            _value = new FixedString64Bytes(value);
        }

        public bool Equals(DiagnosticId other) => _value.Equals(other._value);
        public override bool Equals(object obj) => obj is DiagnosticId other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
    }

    public static class DiagnosticIds
    {
        public static readonly DiagnosticId InvariantFailed = new DiagnosticId("CORE.INVARIANT.FAILED");
        public static readonly DiagnosticId StableIdCollision = new DiagnosticId("CORE.ID.COLLISION");
        public static readonly DiagnosticId TimeOverflow = new DiagnosticId("SIM.TIME.OVERFLOW");
        public static readonly DiagnosticId SaveCorrupt = new DiagnosticId("SAVE.CONTAINER.CORRUPT");
    }

    public sealed class InvariantViolationException : Exception
    {
        public InvariantViolationException(
            DiagnosticId diagnosticId,
            string domain,
            long simulationMinute,
            string message,
            SaveEntityId? entityId = null)
            : base(Format(diagnosticId, domain, simulationMinute, message, entityId))
        {
            DiagnosticId = diagnosticId;
            Domain = domain ?? string.Empty;
            SimulationMinute = simulationMinute;
            EntityId = entityId;
        }

        public DiagnosticId DiagnosticId { get; }
        public string Domain { get; }
        public long SimulationMinute { get; }
        public SaveEntityId? EntityId { get; }

        private static string Format(
            DiagnosticId diagnosticId,
            string domain,
            long minute,
            string message,
            SaveEntityId? entityId)
        {
            var entity = entityId.HasValue ? $" entity={entityId.Value}" : string.Empty;
            return $"[{diagnosticId}] domain={domain} minute={minute}{entity}: {message}";
        }
    }

    public static class Invariant
    {
        public static void Require(
            bool condition,
            DiagnosticId diagnosticId,
            string domain,
            long simulationMinute,
            string message,
            SaveEntityId? entityId = null)
        {
            if (!condition)
            {
                throw new InvariantViolationException(
                    diagnosticId,
                    domain,
                    simulationMinute,
                    message,
                    entityId);
            }
        }
    }
}
