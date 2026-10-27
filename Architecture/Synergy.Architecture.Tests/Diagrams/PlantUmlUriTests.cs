using PlantUml.Net;
using Synergy.Architecture.Diagrams.Markdown;

namespace Synergy.Architecture.Tests.Diagrams;

public class PlantUmlUriTests
{
    private const string source = "@startmindmap\n+ Example\n++ Child\n@endmindmap\n";

    [Fact]
    public void UrisAreEquivalent_WhenOnlyLineEndingsDiffer()
    {
        var lf = Render(source);
        var crlf = Render(source.Replace("\n", "\r\n"));

        Assert.NotEqual(lf, crlf);
        Assert.True(PlantUmlUri.AreEquivalent(crlf, lf));
    }

    [Fact]
    public void UrisAreNotEquivalent_WhenDiagramSourceDiffers()
    {
        var current = Render(source);
        var generated = Render(source.Replace("Child", "Other"));

        Assert.False(PlantUmlUri.AreEquivalent(current, generated));
    }

    [Fact]
    public void UrisAreNotEquivalent_WhenServerAddressDiffers()
    {
        var https = Render(source);
        var http = https.Replace("https://", "http://");

        Assert.False(PlantUmlUri.AreEquivalent(http, https));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not a uri")]
    [InlineData("https://www.plantuml.com/plantuml/png/!!!")]
    public void UrisAreNotEquivalent_WhenCurrentUriIsInvalid(string current)
    {
        Assert.False(PlantUmlUri.AreEquivalent(current, Render(source)));
    }

    [Fact]
    public void NormalizeSource_ConvertsLineEndingsToLf()
    {
        Assert.Equal("a\nb\nc", PlantUmlUri.NormalizeSource("a\r\nb\rc"));
    }

    private static string Render(string diagram)
    {
        var renderer = new RendererFactory().CreateRenderer(new PlantUmlSettings());
        return renderer.RenderAsUri(diagram, OutputFormat.Png).ToString().Replace("http://", "https://");
    }
}
