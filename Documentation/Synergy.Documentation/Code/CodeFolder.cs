using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Code;

/// <summary>Represents an absolute folder path and provides file and directory navigation helpers.</summary>
public class CodeFolder
{
    /// <summary>Creates a reference to the folder containing the caller's source file.</summary>
    /// <param name="path">A source file path, supplied by the compiler when omitted.</param>
    /// <returns>The containing folder as an absolute folder reference.</returns>
    /// <exception cref="ArgumentException">The supplied path has no containing directory.</exception>
    public static CodeFolder Current([CallerFilePath] string path = "")
        => new(System.IO.Path.GetDirectoryName(path) 
               ?? throw new ArgumentException("Invalid path", nameof(path)));
    
    /// <summary>Gets the absolute folder path.</summary>
    public string Path { get; } 

    /// <summary>Creates a folder reference by resolving a path without checking that the folder exists.</summary>
    /// <param name="path">An absolute path or a path relative to the current working directory.</param>
    /// <exception cref="ArgumentNullException">The path is null.</exception>
    public CodeFolder(string path)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));
        this.Path = System.IO.Path.GetFullPath(path);
    }

    /// <summary>Returns the absolute folder path.</summary>
    /// <returns>The path represented by this folder reference.</returns>
    public override string ToString()
        => Path;

    /// <summary>Creates a file reference by combining this folder's path with a file name or path.</summary>
    /// <param name="fileName">The file name or path to combine with the folder path.</param>
    /// <returns>A file reference; no file is created on disk.</returns>
    public CodeFile File(string fileName) 
        => new(System.IO.Path.Combine(Path, fileName));
    
    /// <summary>Creates a reference to a folder the specified number of levels above this folder.</summary>
    /// <param name="jumps">The nonnegative number of parent levels to navigate.</param>
    /// <returns>The resolved parent folder reference, or this folder's path when zero is specified.</returns>
    public CodeFolder Up(int jumps = 1)
        => new(System.IO.Path.Combine(this.Path, string.Join("/", Enumerable.Repeat("..", jumps))));

    /// <summary>Creates a folder reference by combining this folder's path with another path.</summary>
    /// <param name="subfolder">The subfolder name or path to combine with the folder path.</param>
    /// <returns>A resolved folder reference; no directory is created on disk.</returns>
    public CodeFolder Sub(string subfolder)
        => new(System.IO.Path.Combine(Path, subfolder));
}