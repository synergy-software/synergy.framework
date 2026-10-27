using System.IO.Compression;
using System.Text;
using Synergy.Architecture.Diagrams.Markdown;

namespace Synergy.Architecture.Tests.Diagrams;

public class PlantUmlDiagramsTests
{
    [Fact]
    public void ImagesAreEquivalent_WhenOnlyPlantUmlVersionDiffers()
    {
        var source = "@startmindmap\n+ Example\n@endmindmap";
        var current = CreatePng(source, "1.2026.8beta1", new byte[] { 1, 2, 3 });
        var generated = CreatePng(source, "1.2026.9beta4", new byte[] { 1, 2, 3 });

        Assert.True(PngImageComparer.AreEquivalent(current, generated));
    }

    [Fact]
    public void ImagesAreEquivalent_WhenDiagramSourceDiffers()
    {
        var current = CreatePng("@startmindmap\n+ Old\n@endmindmap", "1.2026.8beta1", new byte[] { 1, 2, 3 });
        var generated = CreatePng("@startmindmap\n+ New\n@endmindmap", "1.2026.9beta4", new byte[] { 1, 2, 3 });

        Assert.False(PngImageComparer.AreEquivalent(current, generated));
    }

    [Fact]
    public void ImagesAreEquivalent_WhenRenderedImageDiffers()
    {
        var source = "@startmindmap\n+ Example\n@endmindmap";
        var current = CreatePng(source, "1.2026.8beta1", new byte[] { 1, 2, 3 });
        var generated = CreatePng(source, "1.2026.9beta4", new byte[] { 1, 2, 4 });

        Assert.False(PngImageComparer.AreEquivalent(current, generated));
    }

    private static byte[] CreatePng(string source, string version, byte[] imageData)
    {
        using var png = new MemoryStream();
        png.Write(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });
        WriteChunk(png, "IHDR", new byte[13]);

        using var compressedText = new MemoryStream();
        using (var compressor = new ZLibStream(compressedText, CompressionLevel.Optimal, leaveOpen: true))
        {
            var text = Encoding.UTF8.GetBytes(source + "\n\n" + version);
            compressor.Write(text);
        }

        var keyword = Encoding.ASCII.GetBytes("plantuml\0");
        var textData = keyword
            .Concat(new byte[] { 1, 0, 0, 0 })
            .Concat(compressedText.ToArray())
            .ToArray();
        WriteChunk(png, "iTXt", textData);
        WriteChunk(png, "IDAT", imageData);
        WriteChunk(png, "IEND", Array.Empty<byte>());

        return png.ToArray();
    }

    private static void WriteChunk(Stream png, string type, byte[] data)
    {
        var length = BitConverter.GetBytes(data.Length);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(length);
        png.Write(length);
        png.Write(Encoding.ASCII.GetBytes(type));
        png.Write(data);
        png.Write(new byte[4]);
    }
}
