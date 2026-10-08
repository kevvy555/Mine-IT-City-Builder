using System.IO;
using MineIT.CityBuilder.Core.Ids;
using MineIT.CityBuilder.Persistence.Binary;

namespace MineIT.CityBuilder.Persistence.Container
{
    public readonly struct DecodedSaveHeader
    {
        public DecodedSaveHeader(SaveHeader header, int bytesRead)
        {
            Header = header;
            BytesRead = bytesRead;
        }

        public SaveHeader Header { get; }
        public int BytesRead { get; }
    }

    public static class SaveHeaderCodec
    {
        private static readonly byte[] Magic = { (byte)'M', (byte)'I', (byte)'C', (byte)'B' };

        public static byte[] Encode(SaveHeader header)
        {
            var writer = new LittleEndianWriter();
            writer.WriteBytes(Magic);
            writer.WriteUInt32(header.ContainerVersion);
            writer.WriteUInt32(header.SaveSchemaVersion);
            writer.WriteString(header.GameVersion);
            writer.WriteString(header.ContentVersion);
            writer.WriteString(header.UniverseCommit);
            writer.WriteUInt64(header.RootSeed);
            writer.WriteString(header.ScenarioId.ToString());
            writer.WriteInt64(header.SimulationMinute);
            writer.WriteInt64(header.CreatedUtcTicks);
            writer.WriteInt64(header.LastSavedUtcTicks);
            writer.WriteUInt64(header.FeatureFlags);
            writer.WriteUInt32(header.SectionCount);

            var withoutChecksum = writer.ToArray();
            writer.WriteUInt32(Crc32.Compute(withoutChecksum));
            return writer.ToArray();
        }

        public static DecodedSaveHeader Decode(byte[] data)
        {
            var reader = new LittleEndianReader(data);
            for (var i = 0; i < Magic.Length; i++)
            {
                if (reader.ReadByte() != Magic[i])
                {
                    throw new InvalidDataException("Not a MineIT City Builder MICB save.");
                }
            }

            var containerVersion = reader.ReadUInt32();
            var schemaVersion = reader.ReadUInt32();
            var gameVersion = reader.ReadString();
            var contentVersion = reader.ReadString();
            var universeCommit = reader.ReadString(256);
            var rootSeed = reader.ReadUInt64();
            var scenarioId = new StableId(reader.ReadString(StableId.CapacityBytes));
            var simulationMinute = reader.ReadInt64();
            var createdTicks = reader.ReadInt64();
            var lastSavedTicks = reader.ReadInt64();
            var featureFlags = reader.ReadUInt64();
            var sectionCount = reader.ReadUInt32();

            var checksumPosition = reader.Position;
            var expectedChecksum = reader.ReadUInt32();
            var actualChecksum = Crc32.Compute(data, 0, checksumPosition);
            if (expectedChecksum != actualChecksum)
            {
                throw new InvalidDataException(
                    $"Save header checksum mismatch. Expected {expectedChecksum:x8}, actual {actualChecksum:x8}.");
            }

            var header = new SaveHeader(
                containerVersion,
                schemaVersion,
                gameVersion,
                contentVersion,
                universeCommit,
                rootSeed,
                scenarioId,
                simulationMinute,
                createdTicks,
                lastSavedTicks,
                featureFlags,
                sectionCount);

            return new DecodedSaveHeader(header, reader.Position);
        }
    }
}
