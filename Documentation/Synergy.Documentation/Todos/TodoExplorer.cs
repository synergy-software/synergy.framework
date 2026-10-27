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
        new TextTodoPattern(),
        new CsprojTodoPattern(),
        new PropsTodoPattern(),
        new TypescriptTodoPattern(),
        new MarkdownTodoPattern(),
        new YamlTodoPattern()
    };

    public static string DebtFor(
        string name,
        CodeFolder from,
        Func<string, bool>? excludePath = null,
        [CallerFilePath] string currentPath = "",
        params TodoPattern[] patterns
    )
    {
        if (patterns.Length == 0)
            patterns = defaultPatterns;

        int count = 0;
        var todos = new StringBuilder();
        excludePath ??= x => false;
        var files = TodoExplorer.GetFilesWithCode(from, excludePath, patterns)
                                .OrderBy(f => f.Path)
                                .ToList();

        foreach (var file in files)
        {
            var fileAdded = false;
            string content = File.ReadAllText(file.Path);
            foreach (Match match in file.Pattern.Matches(content))
            {
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
                string markdownPath = FilePath(filePath);
                string fileName = FileName(filePath);
                todos.AppendLine($"## [{fileName}]({markdownPath})");
            }

            return true;
        }

        string FilePath(string filePath)
        {
            var relativePath = Path.GetRelativePath(Path.GetDirectoryName(currentPath), filePath)
                                   .Replace("\\", "/");
            var markdownPath = string.Join("/", relativePath.Split('/').Select(Uri.EscapeDataString));
            return markdownPath;
        }

        string FileName(string filePath)
        {
            var fileName = Path.GetFileName(filePath)
                               .Replace("\\", "\\\\")
                               .Replace("[", "\\[")
                               .Replace("]", "\\]");
            return fileName;
        }
    }

    private static IEnumerable<(string Path, TodoPattern Pattern)> GetFilesWithCode(
        CodeFolder from,
        Func<string, bool> excludePath,
        TodoPattern[] patterns
    ) => TodoExplorer.GetFilesWithCodeDeep(from.Path, excludePath, patterns);

    private static IEnumerable<(string Path, TodoPattern Pattern)> GetFilesWithCodeDeep(
        string from,
        Func<string, bool> excludePath,
        TodoPattern[] patterns
    )
    {
        foreach (TodoPattern pattern in patterns)
        foreach (var filePath in Directory.GetFiles(from, $"*.{pattern.FileExtension}"))
        {
            if (excludePath(filePath))
                continue;
            
            yield return (Path.GetFullPath(filePath), pattern);
        }

        foreach (var directory in Directory.GetDirectories(from))
        {
            if (excludePath(directory))
                continue;
            
            foreach (var filePath in TodoExplorer.GetFilesWithCodeDeep(directory, excludePath, patterns))
               yield return filePath;
        }
    }
}