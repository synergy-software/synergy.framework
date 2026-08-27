namespace Synergy.Documentation.Annotations;

public static class Note
{
    public static T Comment<T>(this T source, string comment)
        => source;

    public static T DoNothing<T>(this T source, string reason)
        => source;

    public static T DoNotThrowException<T>(this T source, string reason)
        => source;

    public static T Because<T>(this T source, string reason)
        => source;

    public static T Then<T>(this T source, string reason)
        => source;

    public static T But<T>(this T source, string reason)
        => source;

    public static T Therefore<T>(this T source, string reason)
        => source;

    public static T Otherwise<T>(this T source, string reason)
        => source;

    // public static Task<T> Otherwise<T>(this Task<T> source, string reason)
    //     => source;

    public static T Moreover<T>(this T source, string reason)
        => source;
    
    public static T Reference<T>(this T source, string link)
        => source;
    
    public static T Satisfies<T>(this T source, object principle) 
        => source;
    
    /// <summary>
    /// Identity function used purely for readability,
    /// allowing fluent chaining of unrelated statements
    /// (e.g. after `.Because()`/`.Therefore()` reasoning calls).
    /// </summary>
    public static T And<T>(this T source, string? reason = null) 
        => source;
}