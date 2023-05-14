using System.Runtime.CompilerServices;
using Synergy.Contracts;

namespace Synergy.Documentation.Code;

// TODO: Marcin Celej [from: Marcin Celej on: 14-05-2023]: rename to SourceFile

public class CodeFile
{
    public static CodeFolder Current([CallerFilePath] string path = "")
        => new(path);
    
    public string FilePath { get; }
    public string FileName => Path.GetFileName(this.FilePath);
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(this.FilePath);
    public string Extension => Path.GetExtension(this.FilePath).TrimStart('.');

    public CodeFile(string filePath)
    {
        this.FilePath = filePath.OrFailIfWhiteSpace();
    }

    public CodeFolder Folder 
        => new(Path.GetDirectoryName(this.FilePath));
    
    public override string ToString()
        => this.FilePath;

    // public CodeFile RelativeTo(CodeFile file) 
    //     => new(Path.GetRelativePath(file.FilePath, FilePath));

    public CodeFile RelativeTo(CodeFolder folder)
        => new(Path.GetRelativePath(folder.Path, this.FilePath));
}