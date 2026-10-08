using System;

namespace MineIT.CityBuilder.Persistence.Container
{
    public static class SaveSectionIds
    {
        public const uint IdAllocator = 0x4C414449u; // IDAL
        public const uint Scheduler = 0x44484353u;   // SCHD
        public const uint RandomStreams = 0x53474E52u; // RNGS
    }

    public sealed class SaveSection
    {
        public SaveSection(uint typeId, uint schemaVersion, bool required, byte[] data, byte codec = 0)
        {
            TypeId = typeId;
            SchemaVersion = schemaVersion;
            Required = required;
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Codec = codec;
        }

        public uint TypeId { get; }
        public uint SchemaVersion { get; }
        public bool Required { get; }
        public byte[] Data { get; }
        public byte Codec { get; }
    }

    public sealed class MicitySaveContainer
    {
        public MicitySaveContainer(SaveHeader header, SaveSection[] sections)
        {
            Header = header;
            Sections = sections ?? Array.Empty<SaveSection>();
        }

        public SaveHeader Header { get; }
        public SaveSection[] Sections { get; }

        public SaveSection GetRequired(uint typeId)
        {
            for (var i = 0; i < Sections.Length; i++)
            {
                if (Sections[i].TypeId == typeId)
                {
                    return Sections[i];
                }
            }

            throw new InvalidOperationException($"Required save section {typeId:x8} is missing.");
        }
    }
}
