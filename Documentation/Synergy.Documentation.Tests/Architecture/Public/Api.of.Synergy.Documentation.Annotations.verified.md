# Synergy.Documentation.Annotations

## CodeFilePathAttribute (attribute) : Attribute
 - FilePath: string { get; }
 - ctor(
     filePath: string [CallerFilePath, Optional]
   )

## Note (abstract class)
 - Note.Because<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.But<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.Comment<T>(
     source: T?,
     comment: string
   ) : T? [Extension]
 - Note.DoNothing<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.DoNotThrowException<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.Moreover<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.Otherwise<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.Then<T>(
     source: T?,
     reason: string
   ) : T? [Extension]
 - Note.Therefore<T>(
     source: T?,
     reason: string
   ) : T? [Extension]

