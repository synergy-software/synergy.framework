# Verify.Xunit

## VerifyXunit.DerivePathInfo (class) : MulticastDelegate, ICloneable, ISerializable
 - Method: MethodInfo { get; }
 - Target: object [Nullable] { get; }
 - ctor(
     object: object,
     method: IntPtr
   )
 - BeginInvoke(
     sourceFile: string,
     projectDirectory: string,
     type: Type,
     method: MethodInfo,
     callback: AsyncCallback [Nullable],
     object: object [Nullable]
   ) : IAsyncResult [NullableContext]
 - Clone() : object
 - Delegate.Combine(
     a: Delegate?,
     b: Delegate?
   ) : Delegate? [NullableContext]
 - Delegate.Combine(
     delegates: params Delegate[]? [ParamArray]
   ) : Delegate? [NullableContext]
 - Delegate.CreateDelegate(
     type: Type,
     firstArgument: object? [Nullable],
     method: MethodInfo
   ) : Delegate
 - Delegate.CreateDelegate(
     type: Type,
     firstArgument: object? [Nullable],
     method: MethodInfo,
     throwOnBindFailure: bool
   ) : Delegate?
 - Delegate.CreateDelegate(
     type: Type,
     method: MethodInfo
   ) : Delegate
 - Delegate.CreateDelegate(
     type: Type,
     method: MethodInfo,
     throwOnBindFailure: bool
   ) : Delegate?
 - Delegate.CreateDelegate(
     type: Type,
     target: object,
     method: string
   ) : Delegate [RequiresUnreferencedCode]
 - Delegate.CreateDelegate(
     type: Type,
     target: object,
     method: string,
     ignoreCase: bool
   ) : Delegate [RequiresUnreferencedCode]
 - Delegate.CreateDelegate(
     type: Type,
     target: object,
     method: string,
     ignoreCase: bool,
     throwOnBindFailure: bool
   ) : Delegate? [RequiresUnreferencedCode]
 - Delegate.CreateDelegate(
     type: Type,
     target: Type [DynamicallyAccessedMembers],
     method: string
   ) : Delegate
 - Delegate.CreateDelegate(
     type: Type,
     target: Type [DynamicallyAccessedMembers],
     method: string,
     ignoreCase: bool
   ) : Delegate
 - Delegate.CreateDelegate(
     type: Type,
     target: Type [DynamicallyAccessedMembers],
     method: string,
     ignoreCase: bool,
     throwOnBindFailure: bool
   ) : Delegate?
 - Delegate.Remove(
     source: Delegate?,
     value: Delegate?
   ) : Delegate? [NullableContext]
 - Delegate.RemoveAll(
     source: Delegate?,
     value: Delegate?
   ) : Delegate? [NullableContext]
 - DynamicInvoke(
     args: params Object[]? [ParamArray]
   ) : object? [NullableContext]
 - EndInvoke(
     result: IAsyncResult
   ) : PathInfo
 - Equals(
     obj: object? [NotNullWhen]
   ) : bool [NullableContext]
 - GetHashCode() : int
 - GetInvocationList() : Delegate[]
 - GetObjectData(
     info: SerializationInfo,
     context: StreamingContext
   ) : void
 - Invoke(
     sourceFile: string,
     projectDirectory: string,
     type: Type,
     method: MethodInfo
   ) : PathInfo [NullableContext]

## VerifyXunit.UsesVerifyAttribute (attribute) : BeforeAfterTestAttribute
 - ctor()
 - After(
     info: MethodInfo
   ) : void
 - Before(
     info: MethodInfo
   ) : void

## VerifyXunit.Verifier (abstract class)
 - Verifier.DerivePathInfo(
     derivePathInfo: DerivePathInfo
   ) : void
 - Verifier.Throws(
     target: Action,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Throws(
     target: Func<object> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.ThrowsTask(
     target: Func<Task>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.ThrowsTask<T>(
     target: Func<Task<T>>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.ThrowsValueTask(
     target: Func<ValueTask>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.ThrowsValueTask<T>(
     target: Func<ValueTask<T>> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Byte[]? [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Byte[]?,
     settings: VerifySettings? [Optional],
     info: object? [Optional],
     sourceFile: string [Nullable, CallerFilePath, Optional]
   ) : SettingsTask [NullableContext]
 - Verifier.Verify(
     target: FileStream?,
     settings: VerifySettings? [Optional],
     info: object? [Optional],
     sourceFile: string [Nullable, CallerFilePath, Optional]
   ) : SettingsTask [NullableContext]
 - Verifier.Verify(
     target: object? [Nullable],
     rawTargets: IEnumerable<Target>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: object? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Stream? [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Stream?,
     settings: VerifySettings? [Optional],
     info: object? [Optional],
     sourceFile: string [Nullable, CallerFilePath, Optional]
   ) : SettingsTask [NullableContext]
 - Verifier.Verify(
     target: string? [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: string? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Target,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Task<Byte[]>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Task<string>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     target: ValueTask<Byte[]> [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify(
     targets: IEnumerable<Target>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     target: IAsyncEnumerable<T>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     target: Task<T>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     target: Task<T>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     target: ValueTask<T> [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     target: ValueTask<T> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.Verify<T>(
     targets: IEnumerable<T>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyDirectory(
     path: DirectoryInfo [Nullable],
     include: Func<string, bool>? [Nullable, Optional],
     pattern: string? [Optional],
     options: EnumerationOptions? [Optional],
     settings: VerifySettings? [Optional],
     info: object? [Optional],
     fileScrubber: FileScrubber? [Optional],
     sourceFile: string [Nullable, CallerFilePath, Optional]
   ) : SettingsTask [NullableContext]
 - Verifier.VerifyDirectory(
     path: string [Nullable],
     include: Func<string, bool>? [Nullable, Optional],
     pattern: string? [Optional],
     options: EnumerationOptions? [Optional],
     settings: VerifySettings? [Optional],
     info: object? [Optional],
     fileScrubber: FileScrubber? [Optional],
     sourceFile: string [Nullable, CallerFilePath, Optional]
   ) : SettingsTask [NullableContext]
 - Verifier.VerifyFile(
     path: FileInfo,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyFile(
     path: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: Stream? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: string? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: Task<Stream>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: ValueTask<Stream> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyJson(
     target: ValueTask<string> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyTuple(
     expression: Expression<Func<ITuple>>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: Stream? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: string? [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: Task<Stream>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: ValueTask<Stream> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask
 - Verifier.VerifyXml(
     target: ValueTask<string> [Nullable],
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   ) : SettingsTask

## VerifyXunit.VerifyBase (abstract class)
 - ctor(
     settings: VerifySettings? [Nullable, Optional],
     sourceFile: string [CallerFilePath, Optional]
   )
 - Throws(
     target: Action,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Throws(
     target: Func<object> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Throws(
     target: Func<Task>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Throws(
     target: Func<ValueTask>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: Byte[]?,
     extension: string [Nullable],
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: Byte[]?,
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: FileStream?,
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: object? [Nullable],
     rawTargets: IEnumerable<Target>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: object?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: Stream?,
     extension: string [Nullable],
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: Stream?,
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: string? [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: string?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - Verify(
     target: Task<Byte[]>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: Task<string>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     target: ValueTask<Byte[]> [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - Verify(
     targets: IEnumerable<Target>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     target: IAsyncEnumerable<T>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     target: Task<T>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     target: Task<T>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     target: ValueTask<T> [Nullable],
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     target: ValueTask<T> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - Verify<T>(
     targets: IEnumerable<T>,
     extension: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - VerifyDirectory(
     path: DirectoryInfo [Nullable],
     include: Func<string, bool>? [Nullable, Optional],
     pattern: string? [Optional],
     options: EnumerationOptions? [Optional],
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyDirectory(
     path: string [Nullable],
     include: Func<string, bool>? [Nullable, Optional],
     pattern: string? [Optional],
     options: EnumerationOptions? [Optional],
     settings: VerifySettings? [Optional],
     info: object? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyFile(
     path: FileInfo,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - VerifyFile(
     path: string,
     settings: VerifySettings? [Nullable, Optional],
     info: object? [Nullable, Optional]
   ) : SettingsTask
 - VerifyJson(
     target: Stream?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyJson(
     target: string?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyJson(
     target: Task<Stream>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyJson(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyJson(
     target: ValueTask<Stream> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyJson(
     target: ValueTask<string> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyTuple(
     target: Expression<Func<ITuple>>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyXml(
     target: Stream?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyXml(
     target: string?,
     settings: VerifySettings? [Optional]
   ) : SettingsTask [NullableContext]
 - VerifyXml(
     target: Task<Stream>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyXml(
     target: Task<string>,
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyXml(
     target: ValueTask<Stream> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask
 - VerifyXml(
     target: ValueTask<string> [Nullable],
     settings: VerifySettings? [Nullable, Optional]
   ) : SettingsTask

