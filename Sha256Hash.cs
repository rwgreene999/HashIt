using System;
using System.Security.Cryptography;
using System.IO;

public class Sha256Hash : HashAlgorithmBase
{
    public override string ComputeHash(Stream stream)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
}

