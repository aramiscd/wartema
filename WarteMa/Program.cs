using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace WarteMa;

class Program
{
    static readonly int Heartbeat = 600; // millis
    static readonly string MemoryFilePath = "./wartema.json";
    static Dictionary<string, string> _memory = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        try
        {
            var memoryFile = File.ReadAllText(MemoryFilePath);
            _memory =
                JsonSerializer.Deserialize<Dictionary<string, string>>(memoryFile)
                ?? new Dictionary<string, string>();
        }
        catch (Exception)
        {
            // Ignoriere Fehler.  Das ist hier ok.
        }

        while (true)
        {
            if (FilesHaveChanged(args))
            {
                File.WriteAllText(MemoryFilePath, JsonSerializer.Serialize(_memory));
                return;
            }

            Thread.Sleep(Heartbeat);
        }
    }

    static bool FilesHaveChanged(string[] paths)
    {
        var result = false;

        foreach (var path in paths)
        {
            var pathHash = Hash(path);
            var newData = File.ReadAllText(path);
            var newDataHash = Hash(newData);
            var oldDataHash = _memory.GetValueOrDefault(pathHash);
            if (oldDataHash != newDataHash)
            {
                _memory[pathHash] = newDataHash;
                Console.WriteLine(path);
                result = true;
            }
        }

        return result;
    }

    static string Hash(string rawData)
    {
        var builder = new StringBuilder();
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        foreach (var b in bytes) builder.Append(b.ToString("x2"));
        return builder.ToString();
    }
}
