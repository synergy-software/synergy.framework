using System;
using System.Collections.Generic;
using System.Reflection;
using ApprovalTests;
using Synergy.Markdowns;
using Synergy.Markdowns.Test;
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

            documentation.Append(new Markdown.Header2("Remarks"))
                         .Append(new Markdown.Paragraph("This method is intended to be used mainly for testing purposes."));
            
            documentation.Append(new Markdown.Header2("Examples"))
                         .Append(this.GetSampleUsageOfGetFriendlyTypeName())
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on C# primitive types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.primitiveTypes))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on nullable types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.primitiveTypesNullable))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on some more complex types"))
                         .Append(ReflectionExtensionDocumentation.GenerateExampleTable(this.complexTypes));

            var writer = new MarkdownTextWriter(documentation);
            Approvals.Verify(writer);
        }

        private Markdown.Code GetSampleUsageOfGetFriendlyTypeName()
        {
            var type = typeof(string);
            var friendlyName = type.GetFriendlyTypeName();
            Assert.Equal("string", friendlyName);

            return new Markdown.Code()
                   .Line("var type = typeof(string);")
                   .Line("var friendlyName = type.GetFriendlyTypeName();")
                   .Line("Assert.Equal(\"string\", friendlyName);");
        }
        
        private static Markdown.Table GenerateExampleTable(List<Type> types)
        {
            var table = new Markdown.Table("Input type", "Friendly name");
            foreach (var type in types)
            {
                table.Append(type.ToString(), type.GetFriendlyTypeName());
            }

            return table;
        }
        
        [Fact]
        public void GetFriendlyMethodName()
        {
            var documentation = new Markdown.Document();

            documentation.Append(new Markdown.Header1(nameof(MethodInfo) + "." + nameof(ReflectionExtensions.GetFriendlyMethodName) + "() extension method"))
                         .Append(new Markdown.Header2("Definition"))
                         .Append(new Markdown.Paragraph($"Namespace: {typeof(ReflectionExtensions).Namespace}<br/>")
                             .Line($"Assembly: {typeof(ReflectionExtensions).Assembly.GetName().Name}.dll"))
                         .Append(new Markdown.Paragraph("Returns friendly name of a method."));
            
            documentation.Append(new Markdown.Header2("Remarks"))
                         .Append(new Markdown.Paragraph("This method is intended to be used mainly for testing purposes."));
            
            documentation.Append(new Markdown.Header2("Examples"))
                         .Append(this.GetSampleUsageOfGetFriendlyMethodName())
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on System.String public methods"))
                         .Append(this.GenerateExampleTableForGetFriendlyMethodName(typeof(string).GetMethods()))
                         .Append(new Markdown.Paragraph("The following example table shows result of method execution on some other methods"))
                         .Append(this.GenerateExampleTableForGetFriendlyMethodName(new[]{this.GetType().GetMethod(nameof(GenerateExampleTableForGetFriendlyMethodName))}))
                         ;

            var writer = new MarkdownTextWriter(documentation);
            Approvals.Verify(writer);
        }
        
        private Markdown.Code GetSampleUsageOfGetFriendlyMethodName()
        {
            var method = this.GetType().GetMethod(nameof(this.GenerateExampleTableForGetFriendlyMethodName));
            var friendlyName = method.GetFriendlyMethodName();
            Assert.Equal("GenerateExampleTableForGetFriendlyMethodName(MethodInfo[])", friendlyName);

            return new Markdown.Code()
                   .Line("var method = this.GetType().GetMethod(nameof(GenerateExampleTableForGetFriendlyMethodName));")
                   .Line("var friendlyName = method.GetFriendlyMethodName();")
                   .Line("Assert.Equal(\"GenerateExampleTableForGetFriendlyMethodName(MethodInfo[])\", friendlyName);");
        }
        
        public Markdown.Table GenerateExampleTableForGetFriendlyMethodName(MethodInfo[] methods)
        {
            var table = new Markdown.Table("Method", "Friendly name");
            foreach (var method in methods)
            {
                table.Append(method.ToString(), method.GetFriendlyMethodName());
            }

            return table;
        }
    }
}