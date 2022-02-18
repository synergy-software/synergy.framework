using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Synergy.Contracts.Test.Documentation
{
    public class ClassReader
    {
        public static string? ReadMethodBody(string methodName, [CallerFilePath] string sourceFilePath = "")
        {
            var fileContent = File.ReadAllText(sourceFilePath);
            // Regex Check: http://regexstorm.net/tester?p=%28public%7cinternal%7cprotected%7cprivate%29%5cs*%28static%7cabstract%7cvirtual%29*%5cs*%5ba-zA-Z_%5d%5ba-zA-Z_0-9%5d*%3f+Step1Sample%5cs*%3f%5c%28.*%3f%5c%29%28%28%3f%3cindent%3e%5cs*%3f%29%5c%7b%28%3f%3cbody%3e.*%3f%29%5ck%3cindent%3e%5c%7d%29&i=private+static+void+Step1Sample%28%29%0d%0a++++++++%7b%0d%0a++++++++++++Business.Rule%28%22When+withdraw+limit+is+set%2c+withdrawn+amount+cannot+exceed+the+limit%22%29%0d%0a++++++++++++++++++++.Throws%28new+NotImplementedException%28%22NOT+IMPLEMENTED%22%29%29%3b%7b%7d%0d%0a++++++++%7d%0d%0a%0d%0a++++++++private+IEnumerable%3cMarkdown.IElement%3e+Step2MakeItWorking%28%29%0d%0a++++++++%7b%0d%0a++++++++%7d%0d%0a&o=s
            string method = @"(public|internal|protected|private)\s*(static|abstract|virtual)*\s*[a-zA-Z_][a-zA-Z_0-9]*? " + methodName + @"\s*?\(.*?\)((?<indent>\s*?)\{(?<body>.*?)\k<indent>\})";
            var regex = new Regex(method, RegexOptions.Singleline);
            var match = regex.Match(fileContent);

            if (match.Success == false)
                return null;
            
            string? body = match.Groups["body"].Value;
            body = body.Trim('\r', '\n');
            body = UnTabify(body);

            return body;

            string UnTabify(string code)
            {
                Match indentMatch = Regex.Match(code, @"^(?<indent>\s*?)\w");
                if (indentMatch.Success == false)
                    return code;
                
                var indent = indentMatch.Groups["indent"];

                code = Regex.Replace(code, "^" + indent + ".*?", "", RegexOptions.Multiline);
                return code;
            }
        }
    }
}