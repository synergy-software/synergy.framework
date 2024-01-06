using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Code;

public class CodeFile
{
    public static CodeFolder Current([CallerFilePath] string path = "")
        => new(path);
    
    public string FilePath { get; }
    public string FileName => Path.GetFileName(FilePath);
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FilePath);
    public string Extension => Path.GetExtension(FilePath).TrimStart('.');

    public CodeFile(string filePath)
    {
        FilePath = filePath;
    }

    public CodeFolder Folder
        => new(Path.GetDirectoryName(FilePath)
               ?? throw new ArgumentException("Invalid path", nameof(FilePath))
        );

    public override string ToString()
        => FilePath;

    // public CodeFile RelativeTo(CodeFile file) 
    //     => new(Path.GetRelativePath(file.FilePath, FilePath));

    public CodeFile RelativeTo(CodeFolder folder)
        => new(Path.GetRelativePath(folder.Path, FilePath));
}