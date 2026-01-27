using System.IO;
// ECMS-182 Implement CRC64 hashing algorithm

public class Crc64Hash : HashAlgorithmBase
{
    private static readonly ulong[] Table = CreateTable();

    public override string ComputeHash(Stream stream)
    {
        ulong crc = 0xFFFFFFFFFFFFFFFF;

        int b;
        while ((b = stream.ReadByte()) != -1)
        {
            crc = Table[(crc ^ (byte)b) & 0xFF] ^ (crc >> 8);
        }

        crc ^= 0xFFFFFFFFFFFFFFFF;
        return crc.ToString("X16");
    }

    private static ulong[] CreateTable()
    {
        const ulong poly = 0xC96C5795D7870F42;
        var table = new ulong[256];

        for (ulong i = 0; i < 256; i++)
        {
            ulong c = i;
            for (int j = 0; j < 8; j++)
                c = (c & 1) != 0 ? (poly ^ (c >> 1)) : (c >> 1);

            table[i] = c;
        }

        return table;
    }
}
