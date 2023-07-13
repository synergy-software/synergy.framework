using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using PlantUml.Net;
using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams.Api;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Architecture.Diagrams.Extensions;
using Synergy.Reflection;

namespace Synergy.Architecture.Diagrams.Sequence;

public record SequenceDiagramUrl(
    SequenceDiagramActor _actor,
    MethodInfo _root,
    List<Type> _finishOn,
    string? _footer,
    TechnicalBlueprint.DiagramComponents Components
    )
{
    private Type _type => _root.DeclaringType.OrFail(nameof(_root.DeclaringType));
    private readonly List<SequenceDiagramNode> _nodes = new() { new(_actor.Name, _actor.Name, _actor.Archetype, _actor.Note, _actor.Colour) };
    private const Type? unknownType = null;

    public override string ToString()
        => this.GenerateDiagramUrl();

    public string GenerateDiagramUrl()
    {
        var diagram = this.GenerateDiagramContent();
        var factory = new RendererFactory();
        var renderer = factory.CreateRenderer(new PlantUmlSettings());
        var uri = renderer.RenderAsUri(diagram.ToString(), OutputFormat.Png);

        return uri.ToString();
    }

    public StringBuilder GenerateDiagramContent()
    {
        var header = this.GetHeader();

        var sequences = this.GenerateSequences(header);

        var diagram = new StringBuilder();
        diagram.AppendLine("@startuml");
        diagram.AppendLine("skinparam responseMessageBelowArrow true");
        if (header != null)
            diagram.AppendLine($"header {header}");
        if (_footer != null)
            diagram.AppendLine($"footer {_footer}");

        var rootType = Components.Resolve(this._type);
        
        diagram.AppendLine($"title");
        diagram.AppendLine($"{rootType.Name}.{_root.Name}()");
        diagram.AppendLine($"endtitle");
        foreach (var node in this._nodes)
        {
            var colour = "";
            if (node.Colour != null)
                colour = $" #{node.Colour}";
            
            var @as = "";
            if (node.Name != node.FullName)
                @as = $" as \"{node.FullName}\"";
            diagram.AppendLine($"{node.Archetype.ToString().ToLowerInvariant()} {node.Name}{@as}{colour}");
            if (node.Note != null)
            {
                diagram.AppendLine($"/ note over {node.Name}: {node.Note}");
            }
        }

        diagram.Append(sequences);
        diagram.AppendLine("@enduml");
        return diagram;
    }

    private string? GetHeader()
    {
        var rootType = Components.Resolve(_type);
        
        if (!rootType.IsAssignableTo("Microsoft.AspNetCore.Mvc.ControllerBase")) 
            return null;

        var httpMethodAttribute = _root.GetCustomAttribute("Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute");
        if (httpMethodAttribute == null)
            return null;

        var httpRoute = "";
        var routeAttribute = rootType.GetCustomAttribute("Microsoft.AspNetCore.Mvc.RouteAttribute");
        if (routeAttribute != null)
        {
            httpRoute = routeAttribute.GetPropertyValue<string>("Template");
        }
        
        httpRoute = httpRoute.Replace("{version:apiVersion}", "1");
        var httpMethod = httpMethodAttribute.GetPropertyValue<IEnumerable<string>>("HttpMethods").First();
        var template = httpMethodAttribute.GetPropertyValue<string>("Template");
        var request = $"HTTP {httpMethod} {httpRoute}/{template}".TrimEnd('/');
        return request;
    }

    private string GenerateSequences(string? request)
    {
        var rootType = Components.Resolve(this._type);
        var rootTypeName = this.AppendNode(rootType, SequenceDiagramArchetype.Participant);

        var diagram = new StringBuilder();
        
        if(request != null)
            diagram.AppendLine($"{_actor.Name}->{rootTypeName}: [{_root.Name}()] {request}");
        else
            diagram.AppendLine($"{_actor.Name}->{rootTypeName}: {_root.Name}({SequenceDiagramUrl.GetArguments(_root.GetParameters())})");
 
        var note =_root.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault()?.Note;
        if (note != null)
            diagram.AppendLine($"{note}");
        
        diagram.Append(this.NextLevel(rootType, rootTypeName, _root));
        
        if (request != null)
            diagram.AppendLine($"Browser<--{rootTypeName}: HTTP/1.1 200 OK");
        else
            diagram.AppendLine($"{_actor.Name}<--{rootTypeName}");
        
        return diagram.ToString();
    }

    private string NextLevel(Type sourceType, string sourceTypeName, MethodBase sourceMethod)
    {
        var diagram = new StringBuilder();
        var nextSteps = sourceMethod.GetCustomAttributes().Where(a => a is SequenceDiagramElement).Cast<SequenceDiagramElement>();

        if (_finishOn.Contains(sourceType))
        {
            if (nextSteps.Any())
                diagram.AppendLine("note right: ...");
            else
                diagram.AppendLine();

            return diagram.ToString();
        }

        string? activeGroup = null;
        foreach (var nextStep in nextSteps)
        {
            var sequenceDiagramGroup = (nextStep as SequenceDiagramGroup);
            if (sequenceDiagramGroup?.Group != null && sequenceDiagramGroup?.Group != SequenceDiagramGroupType.None)
            {
                var groupName = sequenceDiagramGroup?.Group.ToString().ToLowerInvariant();
                var groupTitle = sequenceDiagramGroup?.GroupHeader;
                var group = $"{groupName} {groupTitle}"; ;

                if(group != activeGroup)
                {
                    diagram.AppendLine(group);
                }
                activeGroup = group;
            }
            else if (activeGroup != null)
            {
                diagram.AppendLine("end");
                activeGroup = null;
            }

            switch (nextStep)
            {
                case SequenceDiagramCallAttribute step:
                    this.InsertCall(step, diagram, sourceType, sourceTypeName);
                    break;
                case SequenceDiagramSelfCallAttribute step:
                    this.InsertSelfCall(step, diagram, sourceType, sourceTypeName);
                    break;
                case SequenceDiagramActivationAttribute step:
                    this.InsertActivation(step, diagram, sourceType, sourceTypeName);
                    break;
                case SequenceDiagramExternalActivationAttribute step:
                    this.InsertActivation(step, diagram, sourceType, sourceTypeName);
                    break;
                case SequenceDiagramDeactivationAttribute step:
                    this.InsertDeactivation(step, diagram, sourceType, sourceTypeName);
                    break;
                case SequenceDiagramExternalCallAttribute step:
                    this.InsertExternalCall(step, diagram, sourceType, sourceTypeName);
                    break;
            }
        }

        if (activeGroup != null)
        {
            diagram.AppendLine("end");
        }
        
        return diagram.ToString();
    }

    private void InsertCall(SequenceDiagramCallAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        var currentType = Components.Resolve(nextStep.Type);
        var currentTypeName= this.AppendNode(currentType, nextStep.Archetype);
        
        var methodInfo = currentType.GetMethod(nextStep.Method,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

        if (methodInfo == null)
        {
            diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Method}");
            return;
        }

        var method = methodInfo ?? throw new Exception($"There is no {currentTypeName}.{nextStep.Method}()");
        var arguments = SequenceDiagramUrl.GetArguments(method.GetParameters());
        diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Method}({arguments})");

        var note = nextStep.Note ?? method.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault()?.Note;
        if (note != null)
        {
            if (note.StartsWith("note") == false)
                note = $"note right: {note}";
            
            diagram.AppendLine($"{note}");
        }

        diagram.Append(this.NextLevel(currentType, currentTypeName, method));

        if (currentTypeName != sourceTypeName)
            diagram.AppendLine($"{sourceTypeName}<--{currentTypeName}: {ApiDescription.GetTypeName(method)}");
    }

    private void InsertSelfCall(SequenceDiagramSelfCallAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        var currentType = sourceType;
        var currentTypeName= this.AppendNode(sourceTypeName, SequenceDiagramArchetype.Participant, currentType);

        var methodInfo = currentType.GetMethod(nextStep.Method,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

        if (methodInfo == null)
        {
            diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Method}");
            if (nextStep.Note != null)
                diagram.AppendLine($"{nextStep.Note}");
            
            return;
        }

        var method = methodInfo ?? throw new Exception($"There is no {currentTypeName}.{nextStep.Method}()");
        var arguments = SequenceDiagramUrl.GetArguments(method.GetParameters());
        diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Method}({arguments})");

        var description = nextStep.Note ?? method.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault()?.Note;
        if (description != null)
            diagram.AppendLine($"{description}");

        diagram.Append(this.NextLevel(currentType, currentTypeName, method));
    }

    private void InsertActivation(SequenceDiagramActivationAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        var currentType = Components.Resolve(nextStep.Type);
        var currentTypeName = this.AppendNode(currentType, nextStep.Archetype);
        
        var constructors = currentType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var ctor =
            constructors.FirstOrDefault(c => c.IsPublic) ??
            constructors.FirstOrDefault(c => c.IsAssembly) ?? 
            constructors.FirstOrDefault();

        var arguments = ctor == null ? "" : SequenceDiagramUrl.GetArguments(ctor.GetParameters());

        var @throw = "";
        if (currentType.IsAssignableTo(typeof(Exception)))
            @throw = "throw "; 
        if (currentTypeName != sourceTypeName)
        {
            diagram.AppendLine($"create {currentTypeName}");
            diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {@throw}new {currentTypeName}({arguments})");
        }

        diagram.AppendLine($"activate {currentTypeName}");
        if (nextStep.Note != null)
            diagram.AppendLine($"{nextStep.Note}");

        if (ctor != null)
            diagram.Append(this.NextLevel(currentType, currentTypeName, ctor));
    }

    private void InsertActivation(SequenceDiagramExternalActivationAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        var currentTypeName = this.AppendNode(nextStep.Type, nextStep.Archetype, SequenceDiagramUrl.unknownType);

        diagram.AppendLine($"create {currentTypeName}");
        diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Constructor}");
        if (nextStep.Note != null)
            diagram.AppendLine($"{nextStep.Note}");
    }

    private void InsertDeactivation(SequenceDiagramDeactivationAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        var currentType = Components.Resolve(nextStep.Type);
        var currentTypeName = this.AppendNode(currentType, SequenceDiagramArchetype.Participant);
        
        diagram.AppendLine($"deactivate {currentTypeName}");
        if (nextStep.Note != null)
            diagram.AppendLine($"{nextStep.Note}");
    }

    private void InsertExternalCall(SequenceDiagramExternalCallAttribute nextStep, StringBuilder diagram, Type sourceType,
        string sourceTypeName)
    {
        var currentTypeName = this.AppendNode(nextStep.Type, nextStep.Archetype, SequenceDiagramUrl.unknownType);
        
        diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {nextStep.Method}");
        if (nextStep.Result != null)
        {
            diagram.AppendLine($"{currentTypeName}->{sourceTypeName}: {nextStep.Result}");
        }
        
        if (nextStep.Note != null)
            diagram.AppendLine($"{nextStep.Note}");
    }

    private static string GetArguments(ParameterInfo[] arguments)
    {
        if (arguments.Length > 2)
            return $"{ApiDescription.GetTypeName(arguments.First())}, ..., {ApiDescription.GetTypeName(arguments.Last())}";

        return String.Join(", ", arguments.Select(p => ApiDescription.GetTypeName(p)));
    }

    private string AppendNode(Type type, SequenceDiagramArchetype archetype)
    {
        return this.AppendNode(ApiDescription.GetTypeName(type), archetype, type);
    }
    
    private string AppendNode(string fullName, SequenceDiagramArchetype archetype, Type? type)
    {
        var name = Regex.Replace(fullName, "[^a-zA-Z]", "_");

        var found = this._nodes.FirstOrDefault(n => n.Name == name);
        if (found != null)
            return found.Name;
        
        string? colour = null;
        string? note = null;
        if (type != null)
        {
            var noteAttribute = type.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault();
            note = noteAttribute?.Note;
            var element = type.GetCustomAttributesBasedOn<SequenceDiagramElementAttribute>().FirstOrDefault();
            if (element != null)
            {
                archetype = element.Archetype;
                note = element.Note;
                colour = element.Colour;
            }
        }
        
        this._nodes.Add(new SequenceDiagramNode(name, fullName, archetype, note, colour));

        return name;
    } 
}

internal record SequenceDiagramNode(string Name, string FullName, SequenceDiagramArchetype Archetype, string? Note, string? Colour);