using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Synergy.Catalogue.Approval
{
    public static class XmlExtensions
    {
        public static Xml AsXml(this string text)
        {
            return new Xml(text);
        }

        public static Xml ToXml<T>(this T toSerialize)
        {
            using var stream = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stream, toSerialize);
            var xml = stream.ToString();
            return new Xml(xml);
        }
    }

    public readonly struct Xml
    {
        private string Content { get; }

        public Xml(string content)
        {
            Content = content;
        }

        /// <inheritdoc />
        public override string ToString() 
            => this.Content;

        public Xml Ignore(params Node[] elements)
        {
            var xml = this;
            foreach (var element in elements)
            {
                xml = xml.Replace(element, "__IGNORED_VALUE__");
            }

            return xml;
        }

        public Xml Replace(Node element, string value)
        {
            var text = this.Content;
            if (element is Attribute)
            {
                var r = new Regex($"{element.Name}=\"(.*?)\"");
                text = r.Replace(text, $"{element.Name}=\"{value}\"");
            }
            else
            {
                var r = new Regex($"<{element.Name}>(.*?)</{element.Name}>");
                text = r.Replace(text, $"<{element.Name}>{value}</{element.Name}>");
            }

            return new Xml(text);
        }
        
        public class Node
        {
            public Node(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        public class Attribute : Node
        {
            public Attribute(string name) : base(name)
            {
            }
        }
    }

    // public static class XmlApprovalsExtensions
    // {
    //     public static void Approve(this Xml xml)
    //     {
    //         ApprovalTests.Approvals.VerifyXml(xml.OrFail(nameof(xml)).ToString());
    //     }
    //
    //     public static void ApproveForScenario(this Xml xml, string scenario)
    //     {
    //         Fail.IfArgumentNull(scenario, nameof(scenario));
    //         
    //         using (ApprovalResults.ForScenario(scenario))
    //         {
    //             xml.Approve();
    //         }
    //     }
    // }
}