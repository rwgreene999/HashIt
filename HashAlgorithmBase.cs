using System.IO;

public interface IHashAlgorithm
{
    string ComputeHash(string filePath);
}

public abstract class HashAlgorithmBase : IHashAlgorithm
{
    public abstract string ComputeHash(Stream stream);

    public string ComputeHash(string filePath)
    {
        using var fs = File.OpenRead(filePath);
        return ComputeHash(fs);
    }
}

