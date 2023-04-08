using System.Reflection;
using System.Xml;
using Synergy.Catalogue.Reflection;
using Synergy.Contracts.Requirements;
using Synergy.Convention.Testing;
using Synergy.Markdowns;

namespace Synergy.Contracts.Test.Documentation
{
    public class ClassDocumentation : Markdown.Document
    {
        public ClassDocumentation(Type type)
        {
            var docTypeName = $"{type.Namespace}.{type.Name}";
            var docsFile = Path.ChangeExtension(typeof(Business).Assembly.Location, "xml");
            var xml = new XmlDocument();
            xml.Load(docsFile);
            var summary = xml.DocumentElement.SelectSingleNode($"//*[@name='T:{docTypeName}']/summary")
                             ?.InnerText.Trim();

            this.Append(new Markdown.Header1(nameof(Business) + " class"))
                .Append(new Markdown.Header2("Definition"))
                .Append(new Markdown.Paragraph($"Namespace: {type.Namespace}<br/>")
                    .Line($"Assembly: {type.Assembly.GetName().Name}.dll"))
                .Append(new Markdown.Paragraph(summary));

            var remarks = xml.DocumentElement.SelectSingleNode($"//*[@name='T:{docTypeName}']/remarks")
                             ?.InnerText.Trim();
            if (String.IsNullOrWhiteSpace(remarks) == false)
            {
                this.Append(new Markdown.Header2("Remarks"))
                    .Append(new Markdown.Paragraph(remarks));
            }

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static)
                              .Where(m => ApiDescription.NotIn(m.Name,
                                  nameof(this.GetType),
                                  nameof(this.ToString),
                                  nameof(Equals),
                                  nameof(object.ReferenceEquals),
                                  nameof(this.GetHashCode)));

            if (methods.Any())
            {
                this.Append(new Markdown.Header2("Methods"));
                var table = new Markdown.Table("Name", "Summary");
                foreach (MethodInfo method in methods)
                {
                    var methodNameInDocumentation = method.ToString();
                    methodNameInDocumentation = methodNameInDocumentation.Substring(methodNameInDocumentation.IndexOf(" ") + 1);
                    var methodSummarry = xml.DocumentElement.SelectSingleNode($"//*[@name='M:{docTypeName}.{methodNameInDocumentation}']/summary")
                                            ?.InnerText.Trim();
                    table.Append(method.GetFriendlyMethodName(), methodSummarry);
                }

                this.Append(table);
            }
        }
    }
}