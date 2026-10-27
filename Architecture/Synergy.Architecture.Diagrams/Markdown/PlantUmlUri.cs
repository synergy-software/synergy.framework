using System.IO.Compression;
using System.Text;

namespace Synergy.Architecture.Diagrams.Markdown;

internal static class PlantUmlUri
{
    private const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";

    internal static string NormalizeSource(string source)
        => source.Replace("\r\n", "\n").Replace("\r", "\n");

    internal static bool AreEquivalent(string current, string generated)
    {
        var currentSeparator = current.LastIndexOf('/');
        var generatedSeparator = generated.LastIndexOf('/');
        if (currentSeparator < 0 || generatedSeparator < 0)
            return false;

        if (current.Substring(0, currentSeparator) != generated.Substring(0, generatedSeparator))
            return false;

        var currentSource = TryDecode(current.Substring(currentSeparator + 1));
        var generatedSource = TryDecode(generated.Substring(generatedSeparator + 1));
        if (currentSource == null || generatedSource == null)
            return false;

        return NormalizeSource(currentSource) == NormalizeSource(generatedSource);
    }

    private static string? TryDecode(string encoded)
    {
        if (encoded.Length == 0)
            return null;

        var compressed = new List<byte>(encoded.Length * 3 / 4 + 3);
        for (var i = 0; i < encoded.Length; i += 4)
        {
            var value = 0;
            for (var j = 0; j < 4; j++)
            {
                var digit = 0;
                if (i + j < encoded.Length)
                {
                    digit = alphabet.IndexOf(encoded[i + j]);
                    if (digit < 0)
                        return null;
                }

                value = (value << 6) | digit;
            }

            compressed.Add((byte)(value >> 16));
            compressed.Add((byte)(value >> 8));
            compressed.Add((byte)value);
        }

        try
        {
            using var input = new MemoryStream(compressed.ToArray());
            using var decompressor = new DeflateStream(input, CompressionMode.Decompress);
            using var output = new MemoryStream();
            decompressor.CopyTo(output);
            return Encoding.UTF8.GetString(output.ToArray()).TrimStart('\uFEFF');
        }
        catch (InvalidDataException)
        {
            return null;
        }
    }
}
