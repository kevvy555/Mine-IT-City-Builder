namespace MineIT.CityBuilder.Persistence.Binary
{
    public static class Crc32
    {
        public static uint Compute(byte[] data) => Compute(data, 0, data.Length);

        public static uint Compute(byte[] data, int offset, int count)
        {
            var crc = 0xFFFFFFFFu;
            var end = offset + count;

            for (var i = offset; i < end; i++)
            {
                crc ^= data[i];
                for (var bit = 0; bit < 8; bit++)
                {
                    var mask = unchecked((uint)-(int)(crc & 1u));
                    crc = (crc >> 1) ^ (0xEDB88320u & mask);
                }
            }

            return ~crc;
        }
    }
}
