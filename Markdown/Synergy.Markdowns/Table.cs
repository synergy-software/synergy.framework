using System.Collections.Generic;
using System.Text;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
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
    }
}