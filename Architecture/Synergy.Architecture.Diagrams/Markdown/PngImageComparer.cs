using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Synergy.Architecture.Diagrams.Markdown;

internal static class PngImageComparer
{
    private static readonly byte[] pngSignature = { 137, 80, 78, 71, 13, 10, 26, 10 };
    private static readonly Regex plantUmlVersion = new(@"\n\n\d+(?:\.\d+)+(?:[A-Za-z][A-Za-z0-9.-]*)?$");

    internal static bool AreEquivalent(byte[] current, byte[] generated)
    {
        if (current.Length < pngSignature.Length ||
            generated.Length < pngSignature.Length ||
            current.AsSpan(0, pngSignature.Length).SequenceEqual(pngSignature) == false ||
            generated.AsSpan(0, pngSignature.Length).SequenceEqual(pngSignature) == false)
        {
            return false;
        }

        var currentOffset = pngSignature.Length;
        var generatedOffset = pngSignature.Length;

        while (TryReadChunk(current, ref currentOffset, out var currentType, out var currentData) &&
               TryReadChunk(generated, ref generatedOffset, out var generatedType, out var generatedData))
        {
            if (currentType != generatedType)
                return false;

            var currentHasSource = TryReadPlantUmlText(
                currentType, currentData, out var currentLanguage, out var currentTranslatedKeyword, out var currentText);
            var generatedHasSource = TryReadPlantUmlText(
                generatedType, generatedData, out var generatedLanguage, out var generatedTranslatedKeyword, out var generatedText);

            if (currentHasSource || generatedHasSource)
            {
                if (currentHasSource == false || generatedHasSource == false ||
                    currentLanguage != generatedLanguage ||
                    currentTranslatedKeyword != generatedTranslatedKeyword ||
                    plantUmlVersion.Replace(currentText, "") != plantUmlVersion.Replace(generatedText, ""))
                {
                    return false;
                }
            }
            else if (currentData.Data.AsSpan().SequenceEqual(generatedData.Data) == false ||
                     currentData.Crc.AsSpan().SequenceEqual(generatedData.Crc) == false)
            {
                return false;
            }

            if (currentType == "IEND")
                return currentOffset == current.Length && generatedOffset == generated.Length;
        }

        return false;
    }

    private static bool TryReadChunk(byte[] image, ref int offset, out string type, out PngChunkData data)
    {
        type = "";
        data = new PngChunkData(Array.Empty<byte>(), Array.Empty<byte>());

        if (offset > image.Length - 12)
            return false;

        var length = ((uint)image[offset] << 24) | ((uint)image[offset + 1] << 16) |
                     ((uint)image[offset + 2] << 8) | image[offset + 3];
        if ((long)length > image.Length - offset - 12)
            return false;

        type = Encoding.ASCII.GetString(image, offset + 4, 4);
        var chunkData = new byte[(int)length];
        Buffer.BlockCopy(image, offset + 8, chunkData, 0, chunkData.Length);
        var crc = new byte[4];
        Buffer.BlockCopy(image, offset + 8 + chunkData.Length, crc, 0, crc.Length);
        data = new PngChunkData(chunkData, crc);
        offset += chunkData.Length + 12;
        return true;
    }

    private static bool TryReadPlantUmlText(
        string type,
        PngChunkData chunk,
        out string language,
        out string translatedKeyword,
        out string text)
    {
        language = "";
        translatedKeyword = "";
        text = "";
        if (type != "iTXt")
            return false;

        var data = chunk.Data;
        var keywordEnd = Array.IndexOf(data, (byte)0);
        if (keywordEnd < 0 ||
            Encoding.ASCII.GetString(data, 0, keywordEnd) != "plantuml" ||
            data.Length < keywordEnd + 5)
        {
            return false;
        }

        var compressionFlag = data[keywordEnd + 1];
        var compressionMethod = data[keywordEnd + 2];
        var languageStart = keywordEnd + 3;
        var languageEnd = Array.IndexOf(data, (byte)0, languageStart);
        if (languageEnd < 0)
            return false;
        language = Encoding.ASCII.GetString(data, languageStart, languageEnd - languageStart);

        var translatedStart = languageEnd + 1;
        var translatedEnd = Array.IndexOf(data, (byte)0, translatedStart);
        if (translatedEnd < 0)
            return false;
        translatedKeyword = Encoding.UTF8.GetString(data, translatedStart, translatedEnd - translatedStart);

        var textStart = translatedEnd + 1;
        var textBytes = new byte[data.Length - textStart];
        Buffer.BlockCopy(data, textStart, textBytes, 0, textBytes.Length);

        if (compressionFlag == 1 && compressionMethod == 0)
        {
            try
            {
                using var compressed = new MemoryStream(textBytes);
                using var decompressor = new ZLibStream(compressed, CompressionMode.Decompress);
                using var result = new MemoryStream();
                decompressor.CopyTo(result);
                textBytes = result.ToArray();
            }
            catch (InvalidDataException)
            {
                return false;
            }
        }
        else if (compressionFlag != 0 || compressionMethod != 0)
        {
            return false;
        }

        text = Encoding.UTF8.GetString(textBytes);
        return true;
    }

    private sealed record PngChunkData(byte[] Data, byte[] Crc);
}
