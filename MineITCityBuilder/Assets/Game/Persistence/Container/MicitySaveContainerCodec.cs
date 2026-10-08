using System;
using System.Collections.Generic;
using System.IO;
using MineIT.CityBuilder.Persistence.Binary;

namespace MineIT.CityBuilder.Persistence.Container
{
    public static class MicitySaveContainerCodec
    {
        private const int TableEntryBytes = 30;

        private readonly struct TableEntry
        {
            public TableEntry(
                uint typeId,
                uint schemaVersion,
                long offset,
                uint compressedLength,
                uint uncompressedLength,
                uint checksum,
                byte codec,
                bool required)
            {
                TypeId = typeId;
                SchemaVersion = schemaVersion;
                Offset = offset;
                CompressedLength = compressedLength;
                UncompressedLength = uncompressedLength;
                Checksum = checksum;
                Codec = codec;
                Required = required;
            }

            public uint TypeId { get; }
            public uint SchemaVersion { get; }
            public long Offset { get; }
            public uint CompressedLength { get; }
            public uint UncompressedLength { get; }
            public uint Checksum { get; }
            public byte Codec { get; }
            public bool Required { get; }
        }

        public static byte[] Encode(SaveHeader header, IReadOnlyList<SaveSection> sections)
        {
            if (sections == null)
            {
                throw new ArgumentNullException(nameof(sections));
            }

            var resolvedHeader = header.WithSectionCount(checked((uint)sections.Count));
            var headerBytes = SaveHeaderCodec.Encode(resolvedHeader);
            var payloadOffset = checked((long)headerBytes.Length + (long)sections.Count * TableEntryBytes);
            var entries = new TableEntry[sections.Count];

            for (var i = 0; i < sections.Count; i++)
            {
                var section = sections[i];
                if (section.Codec != 0)
                {
                    throw new NotSupportedException($"Save codec {section.Codec} is not supported in container v1.");
                }

                var length = checked((uint)section.Data.Length);
                entries[i] = new TableEntry(
                    section.TypeId,
                    section.SchemaVersion,
                    payloadOffset,
                    length,
                    length,
                    Crc32.Compute(section.Data),
                    section.Codec,
                    section.Required);
                payloadOffset = checked(payloadOffset + length);
            }

            var writer = new LittleEndianWriter();
            writer.WriteBytes(headerBytes);
            for (var i = 0; i < entries.Length; i++)
            {
                WriteEntry(writer, entries[i]);
            }

            for (var i = 0; i < sections.Count; i++)
            {
                writer.WriteBytes(sections[i].Data);
            }

            return writer.ToArray();
        }

        public static MicitySaveContainer Decode(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var decodedHeader = SaveHeaderCodec.Decode(data);
            var header = decodedHeader.Header;

            if (header.ContainerVersion != SaveHeader.CurrentContainerVersion)
            {
                throw new InvalidDataException(
                    $"Unsupported MICB container version {header.ContainerVersion}.");
            }

            var tableByteCount = checked((long)header.SectionCount * TableEntryBytes);
            if (decodedHeader.BytesRead + tableByteCount > data.Length)
            {
                throw new InvalidDataException("Save section table extends beyond the file.");
            }

            var tableBytes = new byte[checked((int)tableByteCount)];
            Buffer.BlockCopy(data, decodedHeader.BytesRead, tableBytes, 0, tableBytes.Length);
            var tableReader = new LittleEndianReader(tableBytes);
            var entries = new TableEntry[checked((int)header.SectionCount)];

            for (var i = 0; i < entries.Length; i++)
            {
                entries[i] = ReadEntry(tableReader);
            }

            var sections = new SaveSection[entries.Length];
            var seenTypes = new HashSet<uint>();

            for (var i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                if (!seenTypes.Add(entry.TypeId))
                {
                    throw new InvalidDataException($"Duplicate save section type {entry.TypeId:x8}.");
                }

                if (entry.Codec != 0 || entry.CompressedLength != entry.UncompressedLength)
                {
                    throw new InvalidDataException(
                        $"Unsupported or inconsistent codec for section {entry.TypeId:x8}.");
                }

                if (entry.Offset < 0 ||
                    entry.Offset > data.Length ||
                    entry.CompressedLength > data.Length - entry.Offset)
                {
                    throw new InvalidDataException($"Save section {entry.TypeId:x8} has invalid bounds.");
                }

                var payload = new byte[checked((int)entry.CompressedLength)];
                Buffer.BlockCopy(data, checked((int)entry.Offset), payload, 0, payload.Length);

                var checksum = Crc32.Compute(payload);
                if (checksum != entry.Checksum)
                {
                    throw new InvalidDataException(
                        $"Save section {entry.TypeId:x8} checksum mismatch.");
                }

                sections[i] = new SaveSection(
                    entry.TypeId,
                    entry.SchemaVersion,
                    entry.Required,
                    payload,
                    entry.Codec);
            }

            return new MicitySaveContainer(header, sections);
        }

        private static void WriteEntry(LittleEndianWriter writer, TableEntry entry)
        {
            writer.WriteUInt32(entry.TypeId);
            writer.WriteUInt32(entry.SchemaVersion);
            writer.WriteInt64(entry.Offset);
            writer.WriteUInt32(entry.CompressedLength);
            writer.WriteUInt32(entry.UncompressedLength);
            writer.WriteUInt32(entry.Checksum);
            writer.WriteByte(entry.Codec);
            writer.WriteBool(entry.Required);
        }

        private static TableEntry ReadEntry(LittleEndianReader reader)
        {
            return new TableEntry(
                reader.ReadUInt32(),
                reader.ReadUInt32(),
                reader.ReadInt64(),
                reader.ReadUInt32(),
                reader.ReadUInt32(),
                reader.ReadUInt32(),
                reader.ReadByte(),
                reader.ReadBool());
        }
    }
}
