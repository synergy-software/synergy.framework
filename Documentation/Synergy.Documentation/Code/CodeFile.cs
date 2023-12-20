﻿using System.Runtime.CompilerServices;
using Synergy.Contracts;

namespace Synergy.Documentation.Code;

// TODO: Marcin Celej [from: Marcin Celej on: 14-05-2023]: rename to SourceFile

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
        // TODO: Marcin Celej [from: Marcin Celej on: 26-07-2023]: fix contract check here
        FilePath = filePath.OrFailIfWhiteSpace(nameof(filePath));
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