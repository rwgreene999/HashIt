using System;
using System.Security.Cryptography;
using System.IO;

public class Sha512Hash : HashAlgorithmBase
{
    public override string ComputeHash(Stream stream)
    {
        using var sha = SHA512.Create();
        var hash = sha.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
}

