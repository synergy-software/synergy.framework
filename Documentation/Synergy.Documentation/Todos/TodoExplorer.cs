using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Synergy.Documentation.Code;

namespace Synergy.Documentation.Todos;

public static class TodoExplorer
{
    private static readonly Regex _csharpComment = new("\\/\\/\\s*TODO");
    private static readonly Regex _gherkinComment = new("#\\s*TODO");
    private static readonly Regex _txtComment = new("TODO");

    public static string DebtFor(string name, CodeFolder from, [CallerFilePath] string currentPath = "")
    {
        var debt = new StringBuilder();
        
        debt.AppendLine($"# Technical Debt for {name}");
        debt.AppendLine();

        int count = 0;
        var todos = new StringBuilder();
        foreach (var filePath in TodoExplorer.GetFilesWithCode(from).OrderBy(f => f).ToList())
        {
            var fileAdded = false;
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Allow configuring file extensions with rules of finding TODOs inside
                if (filePath.EndsWith("feature") && TodoExplorer._gherkinComment.Match(line).Success)
                {
                    fileAdded = AddFile(fileAdded, filePath);
                    todos.AppendLine($"- {line.Trim().TrimStart('#').Trim()}");
                    count++;
                }
                
                if (filePath.EndsWith("cs") && TodoExplorer._csharpComment.Match(line).Success)
                {
                    fileAdded = AddFile(fileAdded, filePath);
                    todos.AppendLine($"- {line.Trim().TrimStart('/').Trim()}");
                    count++;
                }
                
                if (filePath.EndsWith("txt") && TodoExplorer._txtComment.Match(line).Success)
                {
                    fileAdded = AddFile(fileAdded, filePath);
                    todos.AppendLine($"- {line.Trim().TrimStart('-').Trim()}");
                    count++;
                }
            }
        }

        debt.AppendLine($"Total: {count}");
        debt.Append(todos);
        return debt.ToString();

        bool AddFile(bool alreadyAdded, string filePath)
        {
            if (alreadyAdded == false)
            {
                todos.AppendLine();
                var relativePath = Path.GetRelativePath(Path.GetDirectoryName(currentPath), filePath).Replace("\\", "/");
                todos.AppendLine($"## [{Path.GetFileName(filePath)}]({relativePath})");
            }

            return true;
        }
    }

    private static IEnumerable<string> GetFilesWithCode(CodeFolder from)
        => TodoExplorer.GetFilesWithCodeDeep(from.Path);

    private static IEnumerable<string> GetFilesWithCodeDeep(string from)
    {
        var files =
            Directory.GetFiles(from, "*.cs").Union(
                Directory.GetFiles(from, "*.feature")
            ).Union(
                Directory.GetFiles(from, "*.txt")
            );
        
        foreach (var filePath in files) 
            yield return Path.GetFullPath(filePath);

        foreach (var directory in Directory.GetDirectories(from))
        foreach (var filePath in TodoExplorer.GetFilesWithCodeDeep(directory))
            yield return filePath;
    }
}