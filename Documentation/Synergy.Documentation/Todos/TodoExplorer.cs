using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Synergy.Documentation.Code;
using Synergy.Documentation.Todos.Patterns;

namespace Synergy.Documentation.Todos;

public static class TodoExplorer
{
    private static readonly TodoPattern[] defaultPatterns =
    {
        new CsharpTodoPattern(),
        new GherkinTodoPattern(),
        new TextTodoPattern()
        // TODO: Marcin Celej [from: Marcin Celej on: 26-12-2023]: Introduce md todo pattern: [//]: # (TODO Convert this markdown to docs as code)
    };

    public static string DebtFor(
        string name,
        CodeFolder from,
        [CallerFilePath] string currentPath = "",
        params TodoPattern[] patterns
    )
    {
        if (patterns.Length == 0)
            patterns = defaultPatterns;

        int count = 0;
        var todos = new StringBuilder();
        var files = TodoExplorer.GetFilesWithCode(from, patterns)
                                .OrderBy(f => f.Path)
                                .ToList();

        foreach (var file in files)
        {
            var fileAdded = false;
            var lines = File.ReadAllLines(file.Path);
            foreach (var line in lines)
            {
                Match match = file.Pattern.Regex.Match(line);
                if (match.Success == false)
                    continue;

                fileAdded = AddFileHeader(fileAdded, file.Path);
                var todo = file.Pattern.TodoExtractor(match);
                todos.AppendLine($"- {todo.Trim()}");
                count++;
            }
        }

        var debt = new StringBuilder();
        debt.AppendLine($"# Technical Debt for {name}");
        debt.AppendLine();
        debt.AppendLine($"Total: {count}");
        debt.Append(todos);
        return debt.ToString();

        bool AddFileHeader(bool alreadyAdded, string filePath)
        {
            if (alreadyAdded == false)
            {
                todos.AppendLine();
                var relativePath = Path.GetRelativePath(Path.GetDirectoryName(currentPath), filePath)
                                       .Replace("\\", "/");
                todos.AppendLine($"## [{Path.GetFileName(filePath)}]({relativePath})");
            }

            return true;
        }
    }

    private static IEnumerable<(string Path, TodoPattern Pattern)> GetFilesWithCode(CodeFolder from, TodoPattern[] patterns)
        => TodoExplorer.GetFilesWithCodeDeep(from.Path, patterns);

    private static IEnumerable<(string Path, TodoPattern Pattern)> GetFilesWithCodeDeep(string from, TodoPattern[] patterns)
    {
        foreach (TodoPattern pattern in patterns)
        foreach (var filePath in Directory.GetFiles(from, $"*.{pattern.FileExtension}"))
            yield return (Path.GetFullPath(filePath), pattern);

        foreach (var directory in Directory.GetDirectories(from))
        foreach (var filePath in TodoExplorer.GetFilesWithCodeDeep(directory, patterns))
            yield return filePath;
    }
}