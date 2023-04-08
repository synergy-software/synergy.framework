using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams.Api;
using Synergy.Contracts;
using Synergy.Reflection;

namespace Synergy.Architecture.Diagrams.Sequence;

public record SequenceDiagram(
    SequenceDiagramActor Actor,
    MethodInfo? Method = null,
    Type[]? FinishOn = null,
    string? FooterText = null,
    TechnicalBlueprint.DiagramComponents? Components = null,
    string? TitleText = null
)
{
    public static SequenceDiagram From<T>()
    {
        var attribute = typeof(T).GetCustomAttributesBasedOn<SequenceDiagramElementAttribute>()
                                 ?.FirstOrDefault();
        var actor = new SequenceDiagramActor(
            typeof(T).Name,
            attribute?.Archetype ?? SequenceDiagramArchetype.Actor,
            attribute?.Note,
            attribute?.Colour
        );
        return SequenceDiagram.From(actor);
    }

    public static SequenceDiagram From(SequenceDiagramActor actor)
        => new(actor, FinishOn: Type.EmptyTypes);

    public SequenceDiagram Calling<T>(string methodName, params Type[] arguments)
        => this.Calling(typeof(T), methodName, arguments);

    public SequenceDiagram Calling<T>(Expression<Action<T>> call)
    {
        var calling = (call.Body as MethodCallExpression).FailIfNull(Violation.Of("This is not a method call"));
        return this.Calling(calling.Method);
    }

    public SequenceDiagram Calling(Type type, string methodName, params Type[] arguments)
    {
        MethodInfo? method;
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic;

        if (arguments.Any())
            method = type.GetMethod(methodName, flags, arguments);
        else
            method = type.GetMethod(methodName, flags);

        method.FailIfNull(Violation.Of("No method {0}.{1}()", type.Name, methodName));

        return this.Calling(method!);
    }

    public SequenceDiagram Calling(MethodInfo method)
        => this with { Method = method.OrFail(nameof(method)) };

    public SequenceDiagram Footer(string footer)
        => this with { FooterText = footer };

    public SequenceDiagram CutOff(params Type[] types)
        => this with { FinishOn = types };

    public SequenceDiagram Title(string title)
        => this with { TitleText = title };

    public string Render()
    {
        var diagrams = new StringBuilder();
        var method = Method;
        var type = Components.Resolve(method.DeclaringType.OrFail(nameof(method.DeclaringType)));
        var finish = FinishOn.ToList();
        var arguments = String.Join(", ",
            method.GetParameters()
                  .Select(a => ApiDescription.GetTypeName(a)));
        var title = TitleText ?? $"{type.Name}.{method.Name}({arguments})";
        diagrams.AppendLine($"##  {title}");
        diagrams.AppendLine();
        diagrams.AppendLine($"**Root type:** `{type.Name}` (from: `{type.Assembly.GetName().Name}`)");
        diagrams.AppendLine();
        diagrams.AppendLine("**Root method:**");
        diagrams.AppendLine("```");
        diagrams.AppendLine($"    {ApiDescription.For(method, withAttributes: false).Replace("  ", "   ")}");
        diagrams.AppendLine("```");
        diagrams.AppendLine();

        var sequenceDiagram = new SequenceDiagramUrl(Actor, method, finish, FooterText, Components);

        diagrams.AppendLine((string)$"![Sequence Diagram for {type.Name}.{method.Name}()]({sequenceDiagram.GenerateDiagramUrl()})");
        diagrams.AppendLine("<!--");
        diagrams.Append(sequenceDiagram.GenerateDiagramContent());
        diagrams.AppendLine("-->");
        diagrams.AppendLine();
        if (this.FooterText != null)
        {
            diagrams.AppendLine(FooterText);
            diagrams.AppendLine();
        }

        return diagrams.ToString();
    }

    public override string ToString()
        => this.Render();
}