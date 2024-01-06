using System.Reflection;
using System.Runtime.CompilerServices;
using Synergy.Documentation.Annotations;

namespace Synergy.Documentation.Code;

public class CodeFile
{
    public static CodeFile Current([CallerFilePath] string path = "")
        => new(path);
    
    public static CodeFile For<T>()
    {
        var attr = typeof(T).GetCustomAttribute<CodeFilePathAttribute>();
        if (attr is null)
            throw new InvalidOperationException($"Type {typeof(T).FullName} is not decorated with [{nameof(CodeFilePathAttribute).Replace("Attribute", "")}] attribute");
        
        return new CodeFile(attr.FilePath);
    }
    
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
    
    public string ReadAllText()
        => File.ReadAllText(FilePath);
}