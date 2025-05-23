﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using PlantUml.Net;

namespace Synergy.Architecture.Diagrams.Markdown;

public static class PlantUmlDiagrams
{
    private static string NL = Environment.NewLine;
    const string disclaimer = "<!-- ← Generated image link. Do NOT modify it manually. -->";
    
    public static void Process(string root, bool links = true, string images = "images")
    {
        foreach (var filePath in PlantUmlDiagrams.GetFilesWithCodeDeep(root))
        {
            var content = File.ReadAllText(filePath);
            var newContent = GenerateDiagrams(content, filePath);
            if (content != newContent)
                File.WriteAllText(filePath, newContent);
        }

        string GenerateDiagrams(string content, string markdownFilePath)
        {
            var r = new Regex(
                $"<!--\\s*```plantuml\\s*(.*?)```\\s*-->(\\s*!\\[(.*?)\\]\\(.*?\\) {disclaimer})?",
                RegexOptions.Singleline
            );

            int diagramNo = 1;
            var s = r.Replace(content,
                match =>
                {
                    var rawPlantUml = match.Groups[1].Value;
                    var name = GetDiagramName(match);
                    var title = GetDiagramTitle(rawPlantUml);
                    var diagramName = name ?? title ?? "diagram " + diagramNo;
                    
                    if (links)
                    {
                        return GenerateDiagramImageAsLink(rawPlantUml, diagramName);
                    }

                    return GenerateDiagramImageAsFile(rawPlantUml, diagramName, markdownFilePath);
                }
            );
            return s;
        }

        string GenerateDiagramImageAsLink(string rawPlantUml, string diagramName)
        {
            var uri = GetDiagramUri(rawPlantUml);
            var newOne = $"<!--{PlantUmlDiagrams.NL}```plantuml{PlantUmlDiagrams.NL}{rawPlantUml}```{PlantUmlDiagrams.NL}-->{PlantUmlDiagrams.NL}![{diagramName}]({uri}) {PlantUmlDiagrams.disclaimer}";
            return newOne;
        }
        
        string GetDiagramUri(string rawPlantUml)
        {
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());
            var uri = renderer.RenderAsUri(rawPlantUml, OutputFormat.Png);
            return uri.ToString().Replace("http://", "https://");
        }
        
        byte[] GetDiagramImage(string rawPlantUml)
        {
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());
            var image = renderer.Render(rawPlantUml, OutputFormat.Png);
            return image;
        }

        string? GetDiagramName(Match match)
        {
            if (string.IsNullOrWhiteSpace(match.Groups[3].Value) == false)
                return match.Groups[3].Value;

            return null;
        }

        string GenerateDiagramImageAsFile(string rawPlantUml, string diagramName, string markdownFilePath)
        {
            var diagramFileName = diagramName;
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                diagramFileName = diagramFileName.Replace(invalidChar.ToString(), "");
            }
            
            byte[] image = GetDiagramImage(rawPlantUml);
            string? markdownFileFolder = Path.GetDirectoryName(markdownFilePath);
            string diagramPath = FindOrCreateDiagramFolder(markdownFileFolder);
            string diagramFilePath = Path.Combine(diagramPath,  diagramFileName + ".png");
            File.WriteAllBytes(diagramFilePath, image);

            var relative = Path.GetRelativePath(markdownFileFolder, diagramFilePath)
                               .Replace("\\", "/")
                               .Replace(" ", "%20");
                    
            var tweaked = $"<!--{PlantUmlDiagrams.NL}```plantuml{PlantUmlDiagrams.NL}{rawPlantUml}```{PlantUmlDiagrams.NL}-->{PlantUmlDiagrams.NL}![{diagramName}]({relative}) {PlantUmlDiagrams.disclaimer}";

            return tweaked;
        }
        
        string? GetDiagramTitle(string rawPlantUml)
        {
            var title = Regex.Match(rawPlantUml, @"title(.*)").Groups[1].Value;
            if (String.IsNullOrWhiteSpace(title))
                return null;
            
            return title.Trim();
        }

        string FindOrCreateDiagramFolder(string? markdownFileFolder)
        {
            string diagramPath = Path.Combine(markdownFileFolder, images);
            if (Directory.Exists(diagramPath) == false)
                Directory.CreateDirectory(diagramPath);
            return diagramPath;
        }
    }

    private static IEnumerable<string> GetFilesWithCodeDeep(string from)
    {
        foreach (var filePath in Directory.GetFiles(from, "*.md"))
            yield return Path.GetFullPath(filePath);

        foreach (var directory in Directory.GetDirectories(from))
        foreach (var filePath in PlantUmlDiagrams.GetFilesWithCodeDeep(directory))
            yield return filePath;
    }
}