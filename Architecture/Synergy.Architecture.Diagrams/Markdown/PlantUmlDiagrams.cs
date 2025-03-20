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
            const string disclaimer = "<!-- ← Generated image link. Do NOT modify it manually. -->";
            var r = new Regex(
                $"<!--\\s*```plantuml\\s*(.*?)```\\s*-->(\\s*!\\[(.*?)\\]\\(http[s]?\\:\\/\\/www\\.plantuml\\.com\\/plantuml\\/png\\/.*?\\) {disclaimer})?",
                RegexOptions.Singleline
            );

            var s = r.Replace(content,
                match =>
                {
                    var rawPlantUml = match.Groups[1].Value;
                    var name = GetDiagramName(match);
                    var uri = GetDiagramUri(rawPlantUml);
                    var newOne = $"<!--\n```plantuml\n{rawPlantUml}```\n-->\n![{name}]({uri}) {disclaimer}";

                    return newOne;
                }
            );
            return s;
        }

        string GetDiagramUri(string rawPlantUml)
        {
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());
            var uri = renderer.RenderAsUri(rawPlantUml, OutputFormat.Png);
            return uri.ToString().Replace("http://", "https://");
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