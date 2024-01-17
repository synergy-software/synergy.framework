namespace Synergy.Behaviours.Testing.Gherkin.File;

internal static class GherkinWriter
{
    public static void Write(string path, string file, string code)
    {
        var destinationFilePath = GherkinReader.FullPathOf(path, file);
        System.IO.File.WriteAllText(destinationFilePath, code);
    }
}