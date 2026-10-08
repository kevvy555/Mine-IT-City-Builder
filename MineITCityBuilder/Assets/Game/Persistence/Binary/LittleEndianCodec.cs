using System;
using System.IO;
using System.Text;

namespace MineIT.CityBuilder.Persistence.Binary
{
    public sealed class LittleEndianWriter
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public int Length => checked((int)_stream.Length);

        public void WriteByte(byte value) => _stream.WriteByte(value);

        public void WriteBool(bool value) => WriteByte((byte)(value ? 1 : 0));

        public void WriteUInt16(ushort value)
        {
            WriteByte((byte)value);
            WriteByte((byte)(value >> 8));
        }

        public void WriteUInt32(uint value)
        {
            WriteByte((byte)value);
            WriteByte((byte)(value >> 8));
            WriteByte((byte)(value >> 16));
            WriteByte((byte)(value >> 24));
        }

        public void WriteInt32(int value) => WriteUInt32(unchecked((uint)value));

        public void WriteUInt64(ulong value)
        {
            WriteUInt32((uint)value);
            WriteUInt32((uint)(value >> 32));
        }

        public void WriteInt64(long value) => WriteUInt64(unchecked((ulong)value));

        public void WriteBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var bytes = Encoding.UTF8.GetBytes(value);
            WriteUInt32(checked((uint)bytes.Length));
            WriteBytes(bytes);
        }

        public byte[] ToArray() => _stream.ToArray();
    }

    public sealed class LittleEndianReader
    {
        private readonly byte[] _data;
        private int _position;

        public LittleEndianReader(byte[] data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public int Position => _position;
        public int Remaining => _data.Length - _position;

        public byte ReadByte()
        {
            Ensure(1);
            return _data[_position++];
        }

        public bool ReadBool()
        {
            var value = ReadByte();
            if (value > 1)
            {
                throw new InvalidDataException($"Invalid boolean byte {value}.");
            }

            return value == 1;
        }

        public ushort ReadUInt16()
        {
            Ensure(2);
            var value = (ushort)(_data[_position] | (_data[_position + 1] << 8));
            _position += 2;
            return value;
        }

        public uint ReadUInt32()
        {
            Ensure(4);
            var value =
                (uint)_data[_position] |
                ((uint)_data[_position + 1] << 8) |
                ((uint)_data[_position + 2] << 16) |
                ((uint)_data[_position + 3] << 24);
            _position += 4;
            return value;
        }

        public int ReadInt32() => unchecked((int)ReadUInt32());

        public ulong ReadUInt64()
        {
            var low = ReadUInt32();
            var high = ReadUInt32();
            return low | ((ulong)high << 32);
        }

        public long ReadInt64() => unchecked((long)ReadUInt64());

        public byte[] ReadBytes(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            Ensure(count);
            var output = new byte[count];
            Buffer.BlockCopy(_data, _position, output, 0, count);
            _position += count;
            return output;
        }

        public string ReadString(int maximumBytes = 16_384)
        {
            var length = ReadUInt32();
            if (length > maximumBytes)
            {
                throw new InvalidDataException($"String length {length} exceeds maximum {maximumBytes} bytes.");
            }

            var bytes = ReadBytes(checked((int)length));
            return Encoding.UTF8.GetString(bytes);
        }

        private void Ensure(int count)
        {
            if (count < 0 || _position > _data.Length - count)
            {
                throw new EndOfStreamException(
                    $"Save buffer ended at {_position}; requested {count} bytes with {Remaining} remaining.");
            }
        }
    }
}
