using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Synergy.Documentation.Code;
using Synergy.Documentation.Todos.Patterns;

namespace Synergy.Documentation.Todos;

/// <summary>Scans source folders for TODO comments and generates Markdown technical debt inventories.</summary>
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

    /// <summary>Recursively scans a folder and groups matching TODO comments by file.</summary>
    /// <param name="name">The project or component name displayed in the report heading.</param>
    /// <param name="from">The root folder to scan.</param>
    /// <param name="excludePath">
    /// An optional predicate that excludes a file or directory when it returns true.
    /// Excluding a directory also excludes its descendants.
    /// </param>
    /// <param name="currentPath">The caller's source file path, used as the base for relative report links.</param>
    /// <param name="patterns">Patterns to use, or an empty array to select the built-in patterns.</param>
    /// <returns>A Markdown report containing the total match count and TODOs grouped by file path.</returns>
    /// <remarks>File system and file reading errors are propagated to the caller.</remarks>
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
            var baseDirectory = Path.GetDirectoryName(currentPath)
                                ?? throw new ArgumentException("The caller file path has no containing directory.", nameof(currentPath));
            var relativePath = Path.GetRelativePath(baseDirectory, filePath)
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