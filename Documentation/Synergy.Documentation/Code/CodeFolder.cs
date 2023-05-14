using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Code;

// TODO: Marcin Celej [from: Marcin Celej on: 14-05-2023]: Rename to SourceFolder

public class CodeFolder
{
    public static CodeFolder Current([CallerFilePath] string path = "")
        => new(System.IO.Path.GetDirectoryName(path));

    public string Path { get; }

    public CodeFolder(string path)
    {
        this.Path = System.IO.Path.GetFullPath(path);
    }

    public override string ToString()
        => this.Path;

    public CodeFile File(string fileName)
        => new(System.IO.Path.Combine(this.Path, fileName));

    public CodeFolder Up(int jumps = 1)
        => new(System.IO.Path.Combine(this.Path, String.Join("\\", Enumerable.Repeat("..", jumps))));
}