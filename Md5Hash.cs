using System; 
using System.Security.Cryptography;
using System.IO;

public class Md5Hash : HashAlgorithmBase
{
    public override string ComputeHash(Stream stream)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
}

