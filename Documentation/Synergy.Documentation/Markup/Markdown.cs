using System.Collections;
using System.Text;
using Synergy.Catalogue;
using Synergy.Documentation.Code;

namespace Synergy.Documentation.Markup
{
    /// <summary>Provides composable elements for building Markdown documents.</summary>
    /// <remarks>Text is rendered as supplied unless an element explicitly trims or formats it; general Markdown escaping is not performed.</remarks>
#if INTERNAL_MARKDOWN
    internal
#else
    public
#endif
        class Markdown
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string NL = Environment.NewLine;

        /// <summary>Marks a document element whose string representation is Markdown.</summary>
        public interface IElement
        {
        }

        /// <summary>Stores an ordered collection of Markdown elements and renders them as a document.</summary>
        public class Document : IEnumerable<IElement>
        {
            private readonly List<IElement> elements = new();

            /// <summary>Adds an element to the end of this document.</summary>
            /// <param name="element">The element to append.</param>
            /// <returns>This document, allowing further elements to be appended fluently.</returns>
            public Document Append(IElement element)
            {
                this.elements.Add(element);
                return this;
            }

            /// <summary>Adds a sequence of elements to the end of this document.</summary>
            /// <param name="newElements">The elements to append in enumeration order.</param>
            /// <returns>This document, allowing further elements to be appended fluently.</returns>
            public Document Append(IEnumerable<IElement> newElements)
            {
                this.elements.AddRange(newElements);
                return this;
            }

            /// <summary>Renders all elements in order, appending a line break after each element.</summary>
            /// <returns>The complete Markdown document.</returns>
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

        /// <summary>Represents a Markdown heading with a specified level.</summary>
        public abstract class Header : IElement
        {
            private readonly int level;
            private readonly string header;

            /// <summary>Initializes a heading with trimmed text.</summary>
            /// <param name="level">The number of hash characters preceding the heading text.</param>
            /// <param name="header">The heading text.</param>
            protected Header(int level, string header)
            {
                this.level = level;
                this.header = header.Trim();
            }

            /// <summary>Renders the heading followed by a line break.</summary>
            /// <returns>The Markdown heading.</returns>
            public override string ToString() => $"{new string('#', this.level)} {this.header}{Markdown.NL}";
        }

        /// <summary>Represents a level-one Markdown heading.</summary>
        public class Header1 : Header
        {
            /// <summary>Creates a level-one heading.</summary>
            /// <param name="header">The heading text, trimmed before rendering.</param>
            public Header1(string header) : base(1, header)
            {
            }
        }

        /// <summary>Represents a level-two Markdown heading.</summary>
        public class Header2 : Header
        {
            /// <summary>Creates a level-two heading.</summary>
            /// <param name="header">The heading text, trimmed before rendering.</param>
            public Header2(string header) : base(2, header)
            {
            }
        }

        /// <summary>Represents a level-three Markdown heading.</summary>
        public class Header3 : Header
        {
            /// <summary>Creates a level-three heading.</summary>
            /// <param name="header">The heading text, trimmed before rendering.</param>
            public Header3(string header) : base(3, header)
            {
            }
        }

        #endregion

        #region Code

        /// <summary>Represents a fenced Markdown code block.</summary>
        public class Code : IElement
        {
            private readonly string? language;
            private readonly string? text;

            /// <summary>Creates a code block with optional text and a language identifier.</summary>
            /// <param name="text">The code text, or null for an initially empty block.</param>
            /// <param name="language">The language identifier; null or whitespace omits the identifier.</param>
            public Code(string? text = null, string? language = "csharp")
            {
                this.language = language?.TrimToNull();
                this.text = text;
            }

            /// <summary>Creates a code block with an additional line, leaving this block unchanged.</summary>
            /// <param name="line">The code line to append.</param>
            /// <returns>A new code block retaining this block's language identifier.</returns>
            public Code Line(string line)
            {
                if (this.text == null)
                    return new Code(line, this.language);
                return new Code(this.text + Markdown.NL + line, this.language);
            }

            /// <summary>Renders the code text enclosed in triple-backtick fences.</summary>
            /// <returns>The fenced code block followed by a line break.</returns>
            public override string ToString() => $"``` {this.language ?? ""}{Markdown.NL}" +
                                                 $"{this.text}{Markdown.NL}" +
                                                 $"```{Markdown.NL}";
        }

        #endregion

        #region Paragraph

        /// <summary>Represents a trimmed block of Markdown paragraph text.</summary>
        public class Paragraph : IElement
        {
            private readonly string text;

            /// <summary>Creates a paragraph, trimming leading and trailing whitespace.</summary>
            /// <param name="text">The paragraph text.</param>
            public Paragraph(string text)
                => this.text = text.Trim();

            /// <summary>Creates a paragraph with an additional line, leaving this paragraph unchanged.</summary>
            /// <param name="line">The line to append.</param>
            /// <returns>A new paragraph containing the combined, trimmed text.</returns>
            public Paragraph Line(string line)
            {
                return new Paragraph(this.text + Markdown.NL + line);
            }

            /// <summary>Renders the paragraph followed by a line break.</summary>
            /// <returns>The paragraph text.</returns>
            public override string ToString() => $"{this.text}{Markdown.NL}";
        }

        #endregion

        #region Quote

        /// <summary>Represents a Markdown block quote composed of stored text lines.</summary>
        public class Quote : IElement
        {
            private readonly List<string> lines;

            /// <summary>Creates a quote from trimmed text, or an empty quote when no text is supplied.</summary>
            /// <param name="text">The initial quote text, or null for an empty quote.</param>
            public Quote(string? text)
            {
                if (text == null)
                {
                    this.lines = new List<string>();
                    return;
                }

                this.lines = new List<string>()
                {
                    text.Trim()
                };
            }

            /// <summary>Appends a trimmed line to this quote.</summary>
            /// <param name="line">The text line to append.</param>
            /// <returns>This quote for further fluent additions.</returns>
            public Quote Line(string line)
            {
                this.lines.Add(line.Trim());
                return this;
            }

            /// <summary>Renders stored lines separated by Markdown block quote prefixes.</summary>
            /// <returns>The block quote followed by a line break.</returns>
            public override string ToString() => $"> {String.Join(Markdown.NL + "> ", this.lines)}{Markdown.NL}";
        }

        #endregion

        #region Table

        /// <summary>Represents a Markdown table with column headers and appended rows.</summary>
        public class Table : IElement
        {
            private readonly string[] headers;
            private readonly List<string[]> rows;

            /// <summary>Creates a table with the supplied column headers.</summary>
            /// <param name="headers">Column header text in display order.</param>
            public Table(params string[] headers)
            {
                this.headers = headers;
                this.rows = new List<string[]>();
            }

            /// <summary>Appends a row without validating its cell count against the headers.</summary>
            /// <param name="cells">Cell text in column order.</param>
            public void Append(params string[] cells)
            {
                this.rows.Add(cells);
            }

            /// <summary>Renders the headers, separator row and data rows using Markdown table syntax.</summary>
            /// <returns>The table with trimmed header and cell text.</returns>
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

        /// <summary>Represents a Markdown image referencing a file path.</summary>
        public class Image : IElement
        {
            private readonly CodeFile _filePath;
            private readonly string _alternateText;

            /// <summary>Creates an image reference with optional alternate text.</summary>
            /// <param name="filePath">The image file reference.</param>
            /// <param name="alternateText">The alternate text, or null to use the file name.</param>
            public Image(CodeFile filePath, string? alternateText = null)
            {
                _filePath = filePath;
                _alternateText = alternateText ?? filePath.FileName;
            }

            /// <summary>Renders the image reference, encoding spaces in the path as percent escapes.</summary>
            /// <returns>The Markdown image reference followed by a line break.</returns>
            public override string ToString()
                => $"![{_alternateText}]({_filePath.ToString().Replace(" ", "%20")}){Markdown.NL}";

            /// <summary>Creates an image reference with a path relative to another file's folder.</summary>
            /// <param name="file">The document file from which the image is referenced.</param>
            /// <returns>A new image reference preserving the alternate text.</returns>
            public Image RelativeFrom(CodeFile file)
                => new(_filePath.RelativeTo(file.Folder), _alternateText);
        }

        #endregion
        
        #region Link
        
        /// <summary>Represents a Markdown link to a file path.</summary>
        public class Link : IElement
        {
            private readonly CodeFile _filePath;
            private readonly string _alternateText;

            /// <summary>Creates a link to a file with optional display text.</summary>
            /// <param name="filePath">The target file reference.</param>
            /// <param name="text">The display text, or null to use the file name.</param>
            /// <returns>A new link to the supplied file.</returns>
            public static Link To(CodeFile filePath, string? text = null)
                => new(filePath, text);
            
            /// <summary>Initializes a link to a file with optional display text.</summary>
            /// <param name="filePath">The target file reference.</param>
            /// <param name="text">The display text, or null to use the file name.</param>
            public Link(CodeFile filePath, string? text = null)
            {
                _filePath = filePath;
                _alternateText = text ?? filePath.FileName;
            }

            /// <summary>Renders the link using forward slashes and percent-escaped spaces in the path.</summary>
            /// <returns>The inline Markdown link.</returns>
            public override string ToString()
                => $"[{_alternateText}]({_filePath.ToString().Replace("\\", "/").Replace(" ", "%20")})";

            /// <summary>Creates a link with a path relative to another file's folder.</summary>
            /// <param name="file">The document file from which the target is linked.</param>
            /// <returns>A new link preserving the display text.</returns>
            public Link RelativeFrom(CodeFile file) 
                => new(_filePath.RelativeTo(file.Folder), _alternateText);
        }
        
        #endregion
    }
}