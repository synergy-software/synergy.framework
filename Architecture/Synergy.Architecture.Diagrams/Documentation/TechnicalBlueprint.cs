using System.Text;
using Synergy.Architecture.Diagrams.Sequence;
using Synergy.Contracts;

namespace Synergy.Architecture.Diagrams.Documentation;

public class TechnicalBlueprint
{
    private string Title { get; set; } = null!;
    private string? _intro;
    private readonly DiagramComponents _components = new();
    private readonly List<SequenceDiagram> diagrams = new();

    public static TechnicalBlueprint Titled(string title)
    {
        var d = new TechnicalBlueprint { Title = title };
        return d;
    }

    public TechnicalBlueprint Intro(string markdown)
    {
        this._intro = markdown;
        return this;
    }

    public TechnicalBlueprint Register<TComponent, TImplementation>()
        where TImplementation : TComponent
    {
        this._components.Register<TComponent, TImplementation>();
        return this;
    }

    public TechnicalBlueprint Register(Type @interface, IServiceProvider services)
    {
        var implementation = services.GetService(@interface) ?? throw new Exception($"There is no {@interface} among registered {nameof(services)}");
        var implementationType = implementation.GetType();
        this.Register(@interface, implementationType);
        return this;
    }

    public TechnicalBlueprint Register(Type @interface, Type implementation)
    {
        this._components.Register(@interface, implementation);
        return this;
    }

    public TechnicalBlueprint Add(params SequenceDiagram[] diagram)
    {
        this.diagrams.AddRange(diagram.OrFail(nameof(diagram)));
        return this;
    }

    public TechnicalBlueprint Add(IEnumerable<SequenceDiagram> diagram)
    {
        this.diagrams.AddRange(diagram.OrFail(nameof(diagram)));
        return this;
    }

    public string Render()
    {
        var docs = new StringBuilder();
        docs.AppendLine($"# {this.Title}");
        docs.AppendLine();

        if (this._intro != null)
        {
            docs.AppendLine(this._intro);
            docs.AppendLine();
        }

        foreach (var diagram in this.diagrams)
        {
            var d = diagram with { Components = this._components };
            docs.AppendLine(d.ToString());
        }

        return docs.ToString();
    }

    public override string ToString()
        => this.Render();
    
    public class DiagramComponents
    {
        private readonly List<Component> _components = new();
        public void Register<TComponent, TImplementation>()
            => this._components.Add(new Component(typeof(TComponent), typeof(TImplementation)));

        public void Register(Type @interface, Type implementation)
            => this._components.Add(new Component(@interface, implementation));
    
        private record Component(Type Interface, Type Implementation);

        public Type Resolve(Type origin)
        {
            Fail.IfArgumentNull(origin, nameof(origin));
            var resolved = this._components.FirstOrDefault(c => c.Interface == origin);

            return resolved?.Implementation ?? origin;
        }
    }
}