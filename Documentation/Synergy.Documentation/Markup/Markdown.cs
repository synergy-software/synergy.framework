using System.Collections;
using System.Text;
using Synergy.Catalogue;
using Synergy.Documentation.Code;

namespace Synergy.Documentation.Markup
{
#if INTERNAL_MARKDOWN
    internal
#else
    public
#endif
        class Markdown
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string NL = Environment.NewLine;

        public interface IElement
        {
        }

        public class Document : IEnumerable<IElement>
        {
            private readonly List<IElement> elements = new();

            public Document Append(IElement element)
            {
                this.elements.Add(element);
                return this;
            }

            public Document Append(IEnumerable<IElement> newElements)
            {
                this.elements.AddRange(newElements);
                return this;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                StringBuilder markdown = new StringBuilder();
                foreach (var element in this.elements)
                {
                    markdown.AppendLine(element.ToString());
                }

                return markdown.ToString();
            }

            /// <inheritdoc />
            public IEnumerator<IElement> GetEnumerator()
                => this.elements.GetEnumerator();

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator()
                => this.GetEnumerator();
        }

        #region Headers

        public abstract class Header : IElement
        {
            private readonly int level;
            private readonly string header;

            protected Header(int level, string header)
            {
                this.level = level;
                this.header = header.Trim();
            }

            public override string ToString() => $"{new string('#', this.level)} {this.header}{Markdown.NL}";
        }

        public class Header1 : Header
        {
            public Header1(string header) : base(1, header)
            {
            }
        }

        public class Header2 : Header
        {
            public Header2(string header) : base(2, header)
            {
            }
        }

        public class Header3 : Header
        {
            public Header3(string header) : base(3, header)
            {
            }
        }

        #endregion

        #region Code

        public class Code : IElement
        {
            private readonly string? language;
            private readonly string? text;

            public Code(string? text = null, string? language = "csharp")
            {
                this.language = language?.TrimToNull();
                this.text = text;
            }

            public Code Line(string line)
            {
                if (this.text == null)
                    return new Code(line, this.language);
                return new Code(this.text + Markdown.NL + line, this.language);
            }

            public override string ToString() => $"``` {this.language ?? ""}{Markdown.NL}" +
                                                 $"{this.text}{Markdown.NL}" +
                                                 $"```{Markdown.NL}";
        }

        #endregion

        #region Paragraph

        public class Paragraph : IElement
        {
            private readonly string text;

            public Paragraph(string text)
                => this.text = text.Trim();

            public Paragraph Line(string line)
            {
                return new Paragraph(this.text + Markdown.NL + line);
            }

            public override string ToString() => $"{this.text}{Markdown.NL}";
        }

        #endregion

        #region Quote

        public class Quote : IElement
        {
            private readonly List<string> lines;

            public Quote(string? text)
            {
                if (text == null)
                    this.lines = new List<string>();

                this.lines = new List<string>()
                {
                    text.Trim()
                };
            }

            public Quote Line(string line)
            {
                this.lines.Add(line.Trim());
                return this;
            }

            public override string ToString() => $"> {String.Join(Markdown.NL + "> ", this.lines)}{Markdown.NL}";
        }

        #endregion

        #region Table

        public class Table : IElement
        {
            private readonly string[] headers;
            private readonly List<string[]> rows;

            public Table(params string[] headers)
            {
                this.headers = headers;
                this.rows = new List<string[]>();
            }

            public void Append(params string[] cells)
            {
                this.rows.Add(cells);
            }

            public override string ToString()
            {
                var table = new StringBuilder();
                foreach (var header in this.headers)
                {
                    table.Append($"| {header.Trim()} ");
                }

                table.AppendLine("|");

                foreach (var header in this.headers)
                {
                    table.Append($"|{new string('-', header.Length + 2)}");
                }

                table.AppendLine("|");

                foreach (var row in this.rows)
                {
                    foreach (var cell in row)
                    {
                        table.Append($"| {cell?.Trim()} ");
                    }

                    table.AppendLine("|");
                }

                return table.ToString();
            }
        }

        #endregion

        #region Image

        public class Image : IElement
        {
            private readonly CodeFile _filePath;
            private readonly string _alternateText;

            public Image(CodeFile filePath, string? alternateText = null)
            {
                _filePath = filePath;
                _alternateText = alternateText ?? filePath.FileName;
            }

            public override string ToString()
                => $"![{_alternateText}]({_filePath.ToString().Replace(" ", "%20")}){Markdown.NL}";

            public Image RelativeTo(CodeFile file)
                => new(_filePath.RelativeTo(file.Folder), _alternateText);
        }

        #endregion
        
        #region Link
        
        public class Link
        {
            private readonly CodeFile _filePath;
            private readonly string _alternateText;

            public static Link To(CodeFile filePath, string? text = null)
                => new(filePath, text);
            
            public Link(CodeFile filePath, string? text = null)
            {
                _filePath = filePath;
                _alternateText = text ?? filePath.FileName;
            }

            public override string ToString()
                => $"[{_alternateText}]({_filePath.ToString().Replace("\\", "/").Replace(" ", "%20")})";

            public Link From(CodeFile file) 
                => new(_filePath.RelativeTo(file.Folder), _alternateText);
        }
        
        #endregion
    }
}