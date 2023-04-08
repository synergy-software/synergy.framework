namespace Synergy.Architecture.Annotations.Diagrams.Sequence;

public enum SequenceDiagramArchetype
{
    Participant,
    Actor,
    
    /// <summary>
    /// A Boundary is a stereotyped Object that models some system boundary, typically a user interface screen.
    /// </summary>
    Boundary,
    
    /// <summary>
    /// A Control is a stereotyped Object that models a controlling entity or manager.
    /// A Control organizes and schedules other activities and elements, typically in Analysis (including Robustness), Sequence and Communication diagrams.
    /// It is the controller of the Model-View-Controller Pattern.
    /// </summary>
    Control,
    
    /// <summary>
    /// An Entity is a stereotyped Object that models a store or persistence mechanism that captures the information or knowledge in a system.
    /// </summary>
    Entity,
    Database,
    Collections,
    Queue
}