using System.Text.RegularExpressions;
using PlantUml.Net;

namespace Synergy.Architecture.Diagrams.Markdown;

public static class PlantUmlDiagrams
{
    public static void Process(string root)
    {
        foreach (var filePath in PlantUmlDiagrams.GetFilesWithCodeDeep(root))
        {
            var content = File.ReadAllText(filePath);
            var newContent = GenerateDiagrams(content);
            if (content != newContent)
                File.WriteAllText(filePath, newContent);
        }

        string GenerateDiagrams(string content)
        {
            var r = new Regex(
                "<!--\\s*```plantuml\\s*(.*?)```\\s*-->(\\s*!\\[(.*?)\\]\\(http\\:\\/\\/www\\.plantuml\\.com\\/plantuml\\/png\\/.*?\\))?",
                RegexOptions.Singleline
            );

            var s = r.Replace(content,
                match =>
                {
                    var rawPlantUml = match.Groups[1].Value;
                    var name = GetDiagramName(match);
                    var uri = GetDiagramUri(rawPlantUml);
                    var newOne = $"<!--\n```plantuml\n{rawPlantUml}```\n-->\n![{name}]({uri}) <!-- ← Generated image link. Do NOT modify it manually. -->";

                    return newOne;
                }
            );
            return s;
        }

        Uri GetDiagramUri(string rawPlantUml)
        {
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());
            var uri = renderer.RenderAsUri(rawPlantUml, OutputFormat.Png);
            return uri;
        }

        string GetDiagramName(Match match)
        {
            if (string.IsNullOrWhiteSpace(match.Groups[3].Value) == false)
                return match.Groups[3].Value;

            return "diagram";
        }
    }

    private static IEnumerable<string> GetFilesWithCodeDeep(string from)
    {
        foreach (var filePath in Directory.GetFiles(from, "*.md"))
            yield return Path.GetFullPath(filePath);

        foreach (var directory in Directory.GetDirectories(from))
        foreach (var filePath in PlantUmlDiagrams.GetFilesWithCodeDeep(directory))
            yield return filePath;
    }
}