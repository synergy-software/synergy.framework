using System;
using System.Collections.Generic;
using ApprovalTests;
using Synergy.Catalogue.Approval;
using Synergy.Catalogue.Markdowns;
using Xunit;

namespace Synergy.Catalogue.Reflection.Tests
{
    public class ReflectionExtensionDocumentation
    {
        private readonly List<Type> primitiveTypes = new List<Type>()
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(char),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(bool),
        };
        
        private readonly List<Type> primitiveTypesNullable = new List<Type>()
        {
            typeof(byte?),
            typeof(sbyte?),
            typeof(short?),
            typeof(ushort?),
            typeof(int?),
            typeof(uint?),
            typeof(long?),
            typeof(ulong?),
            typeof(char?),
            typeof(float?),
            typeof(double?),
            typeof(decimal?),
            typeof(bool?),
        };
        
        private readonly List<Type> complexTypes = new List<Type>()
        {
            typeof(object),
            typeof(string),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(List<int>),
            typeof(int[]),
            typeof(Dictionary<string, long>),
        };
        
        [Fact]
        public void GetFriendlyTypeName()
        {
            var documentation = new Markdown.Document();

            documentation.Append(new Markdown.Header1(nameof(Type) + "." + nameof(ReflectionExtensions.GetFriendlyTypeName) + "() extension method"))
                         .Append(new Markdown.Header2("Definition"))
                         .Append(new Markdown.Paragraph($"Namespace: {typeof(ReflectionExtensions).Namespace}<br/>")
                             .Line($"Assembly: {typeof(ReflectionExtensions).Assembly.GetName().Name}.dll"))
                         .Append(new Markdown.Paragraph("Returns friendly name of Type."));

            documentation.Append(new Markdown.Header2("Examples"))
                         .Append(new Markdown.Code("var type = typeof(string);")
                             .Line($"var friendlyName = type.{nameof(ReflectionExtensions.GetFriendlyTypeName)}();"))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on C# primitive types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.primitiveTypes))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on nullable types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.primitiveTypesNullable))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on some more complex types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.complexTypes));

            documentation.Append(new Markdown.Header2("Remarks"))
                         .Append(new Markdown.Paragraph("This method is intended to be used mainly for testing purposes."));
            
            var writer = new MarkdownTextWriter(documentation);
            Approvals.Verify(writer);
        }

        private static Markdown.Table GenerateExampleTable(List<Type> types)
        {
            var table = new Markdown.Table("Input type", "Friendly name");
            foreach (Type type in types)
            {
                table.Append(type.ToString(), type.GetFriendlyTypeName());
            }
            return table;
        }
    }
}