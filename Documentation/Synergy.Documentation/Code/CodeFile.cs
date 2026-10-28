using System.Reflection;
using System.Runtime.CompilerServices;
using Synergy.Documentation.Annotations;

namespace Synergy.Documentation.Code;

/// <summary>Represents a source file path and provides helpers for documentation links and file access.</summary>
public class CodeFile
{
    /// <summary>Creates a file reference for the caller's source file or an explicitly supplied path.</summary>
    /// <param name="path">The file path, supplied by the compiler when omitted.</param>
    /// <returns>A file reference containing the supplied path.</returns>
    public static CodeFile Current([CallerFilePath] string path = "")
        => new(path);
    
    /// <summary>Creates a file reference from the source path annotation on a type.</summary>
    /// <typeparam name="T">The type decorated with <see cref="CodeFilePathAttribute"/>.</typeparam>
    /// <returns>A file reference containing the annotated path.</returns>
    /// <exception cref="InvalidOperationException">The type has no source path annotation.</exception>
    /// <remarks>The annotation must have been emitted by compiling its usage with DOCUMENTATION defined.</remarks>
    public static CodeFile For<T>()
    {
        var attr = typeof(T).GetCustomAttribute<CodeFilePathAttribute>();
        if (attr is null)
            throw new InvalidOperationException($"Type {typeof(T).FullName} is not decorated with [{nameof(CodeFilePathAttribute).Replace("Attribute", "")}] attribute");
        
        return new CodeFile(attr.FilePath);
    }
    
    /// <summary>Gets the stored file path, which may be absolute or relative.</summary>
    public string FilePath { get; }
    /// <summary>Gets the file name, including its extension.</summary>
    public string FileName => Path.GetFileName(FilePath);
    /// <summary>Gets the file name without its extension.</summary>
    public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FilePath);
    /// <summary>Gets the file extension without a leading dot.</summary>
    public string Extension => Path.GetExtension(FilePath).TrimStart('.');

    /// <summary>Creates a file reference without normalizing the path or checking that the file exists.</summary>
    /// <param name="filePath">The path to store.</param>
    public CodeFile(string filePath)
    {
        FilePath = filePath;
    }

    /// <summary>Gets the containing folder as an absolute folder reference.</summary>
    /// <exception cref="ArgumentException">The file path has no containing directory.</exception>
    public CodeFolder Folder
        => new(Path.GetDirectoryName(FilePath)
               ?? throw new ArgumentException("Invalid path", nameof(FilePath))
        );

    /// <summary>Returns the stored file path.</summary>
    /// <returns>The path represented by this file reference.</returns>
    public override string ToString()
        => FilePath;

    /// <summary>Creates a file reference with a different extension without renaming the file on disk.</summary>
    /// <param name="extension">The replacement extension, with or without a leading dot.</param>
    /// <returns>A new file reference with the changed extension.</returns>
    public CodeFile ChangeExtension(string extension) 
        => new (Path.ChangeExtension(FilePath, extension));

    /// <summary>Creates a file reference relative to the specified folder.</summary>
    /// <param name="folder">The base folder for the relative path.</param>
    /// <returns>A new file reference containing the relative path, or an absolute path if roots differ.</returns>
    public CodeFile RelativeTo(CodeFolder folder)
        => new(Path.GetRelativePath(folder.Path, FilePath));
    
    /// <summary>Reads the entire contents of the referenced file.</summary>
    /// <returns>The file contents as a string.</returns>
    public string ReadAllText()
        => File.ReadAllText(FilePath);
}