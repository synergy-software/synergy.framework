namespace Synergy.Behaviours.Testing.Gherkin.File;

internal static class GherkinReader
{
    public static string[] ReadAllLinesFrom(string path, string file)
    {
        var gherkinPath = FullPathOf(path, file);
        return GherkinReader.ReadAllLinesFrom(gherkinPath);
    }

    internal static string FullPathOf(string path, string file)
    {
        if (String.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));

        if (String.IsNullOrWhiteSpace(file))
            throw new ArgumentNullException(nameof(file));

        return Path.Combine(PathFor(path), file);
    }

    private static String PathFor(string callerFilePath) 
        => Path.GetDirectoryName(callerFilePath) ?? throw new ArgumentException("Improper path: " + callerFilePath, nameof(callerFilePath));

    private static string[] ReadAllLinesFrom(string gherkinPath)
    {
        if (String.IsNullOrWhiteSpace(gherkinPath))
            throw new ArgumentNullException(nameof(gherkinPath));

        if (System.IO.File.Exists(gherkinPath))
            return System.IO.File.ReadAllLines(gherkinPath);

        string[] gherkins = GherkinReader.CreateDefaultFeatureFile(gherkinPath);

        return gherkins;
    }

    private static string[] CreateDefaultFeatureFile(string gherkinPath)
    {
        string feature = Sentence.FromMethod(Path.GetFileNameWithoutExtension(gherkinPath));
        var gherkins = new[]
        {
            $"Feature: {feature}",
            "",
            "# TODO: Provide scenarios here. Check the sample down here.",
            "",
            "#  Scenario: There can be only one",
            "#    Given there are 3 ninjas",
            "#    And there are more than one ninja alive",
            "#    When Two ninjas meet, they will fight",
            "#    Then one ninja dies (but not me)",
            "#    And there is one ninja less alive"
        };

        using var stream = System.IO.File.CreateText(gherkinPath);
        foreach (string line in gherkins)
        {
            stream.WriteLine(line);
        }

        stream.Close();
        return gherkins;
    }
}