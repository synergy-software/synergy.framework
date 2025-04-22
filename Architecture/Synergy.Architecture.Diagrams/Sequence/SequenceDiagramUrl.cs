using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PlantUml.Net;
using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams.Api;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Architecture.Diagrams.Extensions;
using Synergy.Reflection;

namespace Synergy.Architecture.Diagrams.Sequence;

public class SequenceDiagramUrl
{
    private Type _type => _root.DeclaringType ?? throw new ArgumentNullException(nameof(_root.DeclaringType));
    public SequenceDiagramActor _actor { get; }
    public MethodInfo _root { get; }
    public List<Type> _finishOn { get; }
    public string? _footer { get; }
    public TechnicalBlueprint.DiagramComponents Components { get; }

    private readonly List<SequenceDiagramNode> _nodes;

    public SequenceDiagramUrl(SequenceDiagramActor Actor,
        MethodInfo Root,
        List<Type> FinishOn,
        string? Footer,
        TechnicalBlueprint.DiagramComponents Components)
    {
        this._actor = Actor;
        this._root = Root;
        this._finishOn = FinishOn;
        this._footer = Footer;
        this.Components = Components;
        this._nodes = new List<SequenceDiagramNode> { new(Actor.CodeName, Actor.Name, Actor.Archetype, Actor.Note, Actor.Colour) };
    }

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
        // TODO: Marcin Celej [from: Marcin Celej on: 13-07-2023]: add configuration setting to: autoactivate on
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
            if (node.CodeName != node.FullName)
                @as = $" as \"{node.FullName}\"";
            diagram.AppendLine($"{node.Archetype.ToString().ToLowerInvariant()} {node.CodeName}{@as}{colour}");
            if (node.Note != null)
            {
                diagram.AppendLine($"/ note over {node.CodeName}: {node.Note}");
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
            diagram.AppendLine($"{_actor.CodeName}->{rootTypeName}: [{_root.Name}()] {request}");
        else
            diagram.AppendLine($"{_actor.CodeName}->{rootTypeName}: {_root.Name}({SequenceDiagramUrl.GetArguments(_root.GetParameters())})");
 
        var note =_root.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault()?.Note;
        if (note != null)
            diagram.AppendLine($"{note}");
        
        diagram.Append(this.NextLevel(rootType, rootTypeName, _root));
        
        if (request != null)
            diagram.AppendLine($"{_actor.CodeName}<--{rootTypeName}: HTTP/1.1 200 OK");
        else
            diagram.AppendLine($"{_actor.CodeName}<--{rootTypeName}");
        
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
                    if (activeGroup != null && group.StartsWith("else") == false)
                        diagram.AppendLine("end");
                    
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

        var message = nextStep.Message ?? nextStep.Method;
        if (methodInfo == null)
        {
            diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {message}");
            return;
        }

        var method = methodInfo ?? throw new Exception($"There is no {currentTypeName}.{nextStep.Method}()");
        var arguments = SequenceDiagramUrl.GetArguments(method.GetParameters());
        message = nextStep.Message ?? $"{nextStep.Method}({arguments})";
        diagram.AppendLine($"{sourceTypeName}->{currentTypeName}: {message}");

        var note = nextStep.Note ?? method.GetCustomAttributesBasedOn<SequenceDiagramNoteAttribute>().FirstOrDefault()?.Note;
        if (note != null)
        {
            if (note.StartsWith("note") == false)
                note = $"note right: {note}";
            
            diagram.AppendLine($"{note}");
        }

        diagram.Append(this.NextLevel(currentType, currentTypeName, method));

        if (currentTypeName != sourceTypeName)
        {
            var result = nextStep.Result ?? ApiDescription.GetTypeName(method);
            diagram.AppendLine($"{sourceTypeName}<--{currentTypeName}: {result}");
        }
    }

    private void InsertSelfCall(SequenceDiagramSelfCallAttribute nextStep, StringBuilder diagram, Type sourceType, string sourceTypeName)
    {
        // TODO: Marcin Celej [from: Marcin Celej on: 06-07-2023]: Allow to inline self call
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
        if (currentType.IsAssignableTo(typeof(Exception).FullName!))
            @throw = "throw "; 
        if (currentTypeName != sourceTypeName)
        {
            diagram.AppendLine($"create {currentTypeName}");
            // TODO: Marcin Celej [from: Marcin Celej on: 06-07-2023]: Add configuration setting to generate only 'new' word here without exact ctor mentioned here - to simplify diagram
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

    private void InsertExternalCall(
        SequenceDiagramExternalCallAttribute nextStep,
        StringBuilder diagram,
        Type sourceType,
        string sourceTypeName
    )
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
        var codeName = fullName.CodeName();

        var found = this._nodes.FirstOrDefault(n => n.CodeName == codeName);
        if (found != null)
            return found.CodeName;
        
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
        
        this._nodes.Add(new SequenceDiagramNode(codeName, fullName, archetype, note, colour));

        return codeName;
    }

    public void Deconstruct(out SequenceDiagramActor _actor,
        out MethodInfo _root,
        out List<Type> _finishOn,
        out string? _footer,
        out TechnicalBlueprint.DiagramComponents Components)
    {
        _actor = this._actor;
        _root = this._root;
        _finishOn = this._finishOn;
        _footer = this._footer;
        Components = this.Components;
    }
}

internal class SequenceDiagramNode
{
    public SequenceDiagramNode(string CodeName,
        string FullName,
        SequenceDiagramArchetype Archetype,
        string? Note,
        string? Colour)
    {
        this.CodeName = CodeName;
        this.FullName = FullName;
        this.Archetype = Archetype;
        this.Note = Note;
        this.Colour = Colour;
    }

    public string CodeName { get; }
    public string FullName { get; }
    public SequenceDiagramArchetype Archetype { get; }
    public string? Note { get; }
    public string? Colour { get; }
}