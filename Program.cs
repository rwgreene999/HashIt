using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HashIt
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return 1;
            }

            string? path = null;
            string? hashName = null;
            bool json = false;
            bool csv = false;

            // Simple manual argument parsing
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg.Equals("--hash", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-h", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        Console.WriteLine("Missing value for --hash");
                        return 1;
                    }
                    hashName = args[++i];
                }
                else if (arg.Equals("--json", StringComparison.OrdinalIgnoreCase))
                {
                    json = true;
                }
                else if (arg.Equals("--csv", StringComparison.OrdinalIgnoreCase))
                {
                    csv = true;
                }
                else if (path == null)
                {
                    path = arg;
                }
                else
                {
                    Console.WriteLine($"Unexpected argument: {arg}");
                    ShowUsage();
                    return 1;
                }
            }

            if (path == null)
            {
                Console.WriteLine("Path is required.");
                ShowUsage();
                return 1;
            }

            return Run(path, hashName, json, csv);
        }

        private static void ShowUsage()
        {
            Console.WriteLine("HashIt - file hashing utility");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  HashIt <path> [--hash <name>] [--json] [--csv]");
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine("  <path>           File, wildcard pattern, or folder to hash");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --hash, -h       Specific hash to compute (md5, sha1, sha256, sha512, crc32, crc64)");
            Console.WriteLine("  --json           Output results in JSON format");
            Console.WriteLine("  --csv            Output results in CSV format");
        }

        private static int Run(string path, string? hashName, bool json, bool csv)
        {
            var algorithms = new Dictionary<string, IHashAlgorithm>(StringComparer.OrdinalIgnoreCase)
            {
                ["md5"] = new Md5Hash(),
                ["sha1"] = new Sha1Hash(),
                ["sha256"] = new Sha256Hash(),
                ["sha512"] = new Sha512Hash(),
                ["crc32"] = new Crc32Hash(),
                ["crc64"] = new Crc64Hash()
            };

            var files = ResolveFiles(path);

            if (files.Count == 0)
            {
                Console.WriteLine("No matching files found.");
                return 1;
            }

            var results = new List<Dictionary<string, string>>();

            foreach (var file in files)
            {
                var entry = new Dictionary<string, string>
                {
                    ["file"] = file
                };

                if (!string.IsNullOrWhiteSpace(hashName))
                {
                    if (!algorithms.TryGetValue(hashName, out var algo))
                    {
                        Console.WriteLine($"Unknown hash: {hashName}");
                        return 1;
                    }

                    entry[hashName] = algo.ComputeHash(file);
                }
                else
                {
                    foreach (var kv in algorithms)
                    {
                        entry[kv.Key] = kv.Value.ComputeHash(file);
                    }
                }

                results.Add(entry);
            }

            if (json)
            {
                OutputJson(results);
                return 0;
            }

            if (csv)
            {
                OutputCsv(results);
                return 0;
            }

            // Default console output
            foreach (var entry in results)
            {
                Console.WriteLine($"\nFILE: {entry["file"]}");
                foreach (var kv in entry.Where(k => k.Key != "file"))
                {
                    Console.WriteLine($"{kv.Key.ToUpper()}: {kv.Value}");
                }
            }

            return 0;
        }

        private static List<string> ResolveFiles(string path)
        {
            var results = new List<string>();

            if (Directory.Exists(path))
            {
                results.AddRange(Directory.GetFiles(path));
            }
            else if (path.Contains('*') || path.Contains('?'))
            {
                string? dir = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(dir))
                    dir = Directory.GetCurrentDirectory();

                string pattern = Path.GetFileName(path);
                results.AddRange(Directory.GetFiles(dir, pattern));
            }
            else if (File.Exists(path))
            {
                results.Add(path);
            }

            return results;
        }

        private static void OutputJson(List<Dictionary<string, string>> results)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(results, options);
            Console.WriteLine(json);
        }

        private static void OutputCsv(List<Dictionary<string, string>> results)
        {
            var allKeys = results
                .SelectMany(r => r.Keys)
                .Distinct()
                .ToList();

            Console.WriteLine(string.Join(",", allKeys));

            foreach (var entry in results)
            {
                var row = allKeys.Select(k => entry.TryGetValue(k, out var v) ? v : string.Empty);
                Console.WriteLine(string.Join(",", row));
            }
        }
    }
}
