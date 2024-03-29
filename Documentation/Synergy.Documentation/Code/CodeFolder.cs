﻿using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Code;

public class CodeFolder
{
    public static CodeFolder Current([CallerFilePath] string path = "")
        => new(System.IO.Path.GetDirectoryName(path) 
               ?? throw new ArgumentException("Invalid path", nameof(path)));
    
    public string Path { get; } 

    public CodeFolder(string path)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));
        this.Path = System.IO.Path.GetFullPath(path);
    }

    public override string ToString()
        => Path;

    public CodeFile File(string fileName) 
        => new(System.IO.Path.Combine(Path, fileName));
    
    public CodeFolder Up(int jumps = 1)
        => new(System.IO.Path.Combine(this.Path, string.Join("/", Enumerable.Repeat("..", jumps))));

    public CodeFolder Sub(string subfolder)
        => new(System.IO.Path.Combine(Path, subfolder));
}