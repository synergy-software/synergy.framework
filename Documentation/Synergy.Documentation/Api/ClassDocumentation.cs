using System.Reflection;
using System.Xml;
using Synergy.Catalogue;
using Synergy.Catalogue.Reflection;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Api
{
    /// <summary>
    /// Composes a Markdown document from a type's definition and adjacent XML documentation file.
    /// </summary>
    public class ClassDocumentation : Markdown.Document
    {
        /// <summary>Loads XML documentation and builds sections describing a type and its public methods.</summary>
        /// <param name="type">The type to document.</param>
        /// <remarks>
        /// Reads an XML file with the same base name and directory as the type's assembly.
        /// File access and XML parsing errors are propagated to the caller.
        /// </remarks>
        public ClassDocumentation(Type type)
        {
            var docTypeName = $"{type.Namespace}.{type.Name}";
            var docsFile = Path.ChangeExtension(type.Assembly.Location, "xml");
            var xml = new XmlDocument();
            xml.Load(docsFile);
            var summary = xml.SelectSingleNode($"//*[@name='T:{docTypeName}']/summary")
                             ?.InnerText.Trim();

            this.Append(new Markdown.Header1(type + " class"))
                .Append(new Markdown.Header2("Definition"))
                .Append(new Markdown.Paragraph($"Namespace: {type.Namespace}<br/>")
                    .Line($"Assembly: {type.Assembly.GetName().Name}.dll"));

            if (summary != null)
                this.Append(new Markdown.Paragraph(summary));

            var remarks = xml.SelectSingleNode($"//*[@name='T:{docTypeName}']/remarks")
                             ?.InnerText.Trim();
            if (String.IsNullOrWhiteSpace(remarks) == false)
            {
                this.Append(new Markdown.Header2("Remarks"))
                    .Append(new Markdown.Paragraph(remarks));
            }

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static)
                              .Where(m =>
                                  m.Name.NotIn(
                                      nameof(this.GetType),
                                      nameof(this.ToString),
                                      nameof(Equals),
                                      nameof(object.ReferenceEquals),
                                      nameof(this.GetHashCode))
                              );

            if (methods.Any())
            {
                this.Append(new Markdown.Header2("Methods"));
                var table = new Markdown.Table("Name", "Summary");
                foreach (MethodInfo method in methods)
                {
                    var methodNameInDocumentation = method.ToString()
                                                    ?? throw new InvalidOperationException($"Cannot describe method {method.Name}.");
                    methodNameInDocumentation = methodNameInDocumentation.Substring(methodNameInDocumentation.IndexOf(" ") + 1);
                    var methodSummary = xml.SelectSingleNode($"//*[@name='M:{docTypeName}.{methodNameInDocumentation}']/summary")
                                            ?.InnerText.Trim();
                    table.Append(method.GetFriendlyMethodName(), methodSummary ?? "");
                }

                this.Append(table);
            }
        }
    }
}