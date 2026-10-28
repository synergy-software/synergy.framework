namespace Synergy.Documentation.Annotations;

/// <summary>
/// Provides fluent annotations for documenting the intent and rationale of expressions.
/// </summary>
/// <remarks>
/// All methods return the source unchanged. Annotation arguments are not stored, logged or validated.
/// These methods do not add control flow or exception handling; arguments are evaluated normally.
/// </remarks>
public static class Note
{
    /// <summary>Attaches an explanatory comment to an expression.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="comment">The explanation of the expression.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Comment<T>(this T source, string comment)
        => source;

    /// <summary>Documents why no additional action is needed.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The reason for taking no further action.</param>
    /// <returns>The source value unchanged.</returns>
    public static T DoNothing<T>(this T source, string reason)
        => source;

    /// <summary>Documents why an exception is intentionally not thrown; does not suppress exceptions.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The reason for not throwing an exception.</param>
    /// <returns>The source value unchanged.</returns>
    public static T DoNotThrowException<T>(this T source, string reason)
        => source;

    /// <summary>Documents the reason for an expression or decision.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The rationale for the expression or decision.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Because<T>(this T source, string reason)
        => source;

    /// <summary>Documents a subsequent step in the reasoning without executing an additional action.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The description of the subsequent step.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Then<T>(this T source, string reason)
        => source;

    /// <summary>Documents a contrasting consideration or caveat.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The contrasting consideration or caveat.</param>
    /// <returns>The source value unchanged.</returns>
    public static T But<T>(this T source, string reason)
        => source;

    /// <summary>Documents a conclusion drawn from the preceding reasoning.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The conclusion or its rationale.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Therefore<T>(this T source, string reason)
        => source;

    /// <summary>Documents an alternative consideration without introducing conditional execution.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The description of the alternative consideration.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Otherwise<T>(this T source, string reason)
        => source;

    // public static Task<T> Otherwise<T>(this Task<T> source, string reason)
    //     => source;

    /// <summary>Documents an additional supporting consideration.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">The additional consideration.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Moreover<T>(this T source, string reason)
        => source;
    
    /// <summary>Documents a reference to supporting material without opening or validating the link.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="link">The link to supporting material.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Reference<T>(this T source, string link)
        => source;
    
    /// <summary>Documents a principle the expression is intended to satisfy without checking compliance.</summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="principle">The principle or requirement being referenced.</param>
    /// <returns>The source value unchanged.</returns>
    public static T Satisfies<T>(this T source, object principle) 
        => source;
    
    /// <summary>
    /// Continues a fluent annotation chain with an optional additional explanation.
    /// </summary>
    /// <typeparam name="T">The type of the source value.</typeparam>
    /// <param name="source">The value being annotated.</param>
    /// <param name="reason">An optional explanation for continuing the chain.</param>
    /// <returns>The source value unchanged.</returns>
    public static T And<T>(this T source, string? reason = null) 
        => source;
}