using System.IO;

public class Crc32Hash : HashAlgorithmBase
{
    private static readonly uint[] Table = CreateTable();

    public override string ComputeHash(Stream stream)
    {
        uint crc = 0xFFFFFFFF;

        int b;
        while ((b = stream.ReadByte()) != -1)
        {
            crc = (crc >> 8) ^ Table[(crc ^ (byte)b) & 0xFF];
        }

        crc ^= 0xFFFFFFFF;
        return crc.ToString("X8");
    }

    private static uint[] CreateTable()
    {
        uint poly = 0xEDB88320;
        var table = new uint[256];

        for (uint i = 0; i < 256; i++)
        {
            uint c = i;
            for (int j = 0; j < 8; j++)
                c = (c & 1) != 0 ? (poly ^ (c >> 1)) : (c >> 1);

            table[i] = c;
        }

        return table;
    }
}
