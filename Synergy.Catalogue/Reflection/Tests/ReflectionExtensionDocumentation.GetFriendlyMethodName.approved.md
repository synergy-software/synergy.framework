# MethodInfo.GetFriendlyMethodName() extension method

## Definition

Namespace: Synergy.Catalogue.Reflection<br/>
Assembly: Synergy.Catalogue.dll

Returns friendly name of a method.

## Remarks

This method is intended to be used mainly for testing purposes.

## Examples

``` csharp
var method = this.GetType().GetMethod(nameof(GenerateExampleTableForGetFriendlyMethodName));
var friendlyName = method.GetFriendlyMethodName();
Assert.Equal("GenerateExampleTableForGetFriendlyMethodName(MethodInfo[])", friendlyName);
```

The following example table shows result of method execution on System.String public methods

| Method | Friendly name |
|--------|---------------|
| Int32 LastIndexOf(System.String, Int32) | LastIndexOf(string, int) |
| Int32 LastIndexOf(System.String, Int32, Int32) | LastIndexOf(string, int, int) |
| Int32 LastIndexOf(System.String, System.StringComparison) | LastIndexOf(string, StringComparison) |
| Int32 LastIndexOf(System.String, Int32, System.StringComparison) | LastIndexOf(string, int, StringComparison) |
| Int32 LastIndexOf(System.String, Int32, Int32, System.StringComparison) | LastIndexOf(string, int, int, StringComparison) |
| System.String PadRight(Int32) | PadRight(int) |
| System.String PadRight(Int32, Char) | PadRight(int, char) |
| System.String Remove(Int32, Int32) | Remove(int, int) |
| System.String Remove(Int32) | Remove(int) |
| System.String Replace(System.String, System.String, Boolean, System.Globalization.CultureInfo) | Replace(string, string, bool, CultureInfo) |
| System.String Replace(System.String, System.String, System.StringComparison) | Replace(string, string, StringComparison) |
| System.String Replace(Char, Char) | Replace(char, char) |
| System.String Replace(System.String, System.String) | Replace(string, string) |
| System.String ReplaceLineEndings() | ReplaceLineEndings() |
| System.String ReplaceLineEndings(System.String) | ReplaceLineEndings(string) |
| System.String[] Split(Char, System.StringSplitOptions) | Split(char, StringSplitOptions) |
| System.String[] Split(Char, Int32, System.StringSplitOptions) | Split(char, int, StringSplitOptions) |
| System.String[] Split(Char[]) | Split(char[]) |
| System.String[] Split(Char[], Int32) | Split(char[], int) |
| System.String[] Split(Char[], System.StringSplitOptions) | Split(char[], StringSplitOptions) |
| System.String[] Split(Char[], Int32, System.StringSplitOptions) | Split(char[], int, StringSplitOptions) |
| System.String[] Split(System.String, System.StringSplitOptions) | Split(string, StringSplitOptions) |
| System.String[] Split(System.String, Int32, System.StringSplitOptions) | Split(string, int, StringSplitOptions) |
| System.String[] Split(System.String[], System.StringSplitOptions) | Split(string[], StringSplitOptions) |
| System.String[] Split(System.String[], Int32, System.StringSplitOptions) | Split(string[], int, StringSplitOptions) |
| System.String Substring(Int32) | Substring(int) |
| System.String Substring(Int32, Int32) | Substring(int, int) |
| System.String ToLower() | ToLower() |
| System.String ToLower(System.Globalization.CultureInfo) | ToLower(CultureInfo) |
| System.String ToLowerInvariant() | ToLowerInvariant() |
| System.String ToUpper() | ToUpper() |
| System.String ToUpper(System.Globalization.CultureInfo) | ToUpper(CultureInfo) |
| System.String ToUpperInvariant() | ToUpperInvariant() |
| System.String Trim() | Trim() |
| System.String Trim(Char) | Trim(char) |
| System.String Trim(Char[]) | Trim(char[]) |
| System.String TrimStart() | TrimStart() |
| System.String TrimStart(Char) | TrimStart(char) |
| System.String TrimStart(Char[]) | TrimStart(char[]) |
| System.String TrimEnd() | TrimEnd() |
| System.String TrimEnd(Char) | TrimEnd(char) |
| System.String TrimEnd(Char[]) | TrimEnd(char[]) |
| Boolean Contains(System.String) | Contains(string) |
| Boolean Contains(System.String, System.StringComparison) | Contains(string, StringComparison) |
| Boolean Contains(Char) | Contains(char) |
| Boolean Contains(Char, System.StringComparison) | Contains(char, StringComparison) |
| Int32 IndexOf(Char) | IndexOf(char) |
| Int32 IndexOf(Char, Int32) | IndexOf(char, int) |
| Int32 IndexOf(Char, System.StringComparison) | IndexOf(char, StringComparison) |
| Int32 IndexOf(Char, Int32, Int32) | IndexOf(char, int, int) |
| Int32 IndexOfAny(Char[]) | IndexOfAny(char[]) |
| Int32 IndexOfAny(Char[], Int32) | IndexOfAny(char[], int) |
| Int32 IndexOfAny(Char[], Int32, Int32) | IndexOfAny(char[], int, int) |
| Int32 IndexOf(System.String) | IndexOf(string) |
| Int32 IndexOf(System.String, Int32) | IndexOf(string, int) |
| Int32 IndexOf(System.String, Int32, Int32) | IndexOf(string, int, int) |
| Int32 IndexOf(System.String, System.StringComparison) | IndexOf(string, StringComparison) |
| Int32 IndexOf(System.String, Int32, System.StringComparison) | IndexOf(string, int, StringComparison) |
| Int32 IndexOf(System.String, Int32, Int32, System.StringComparison) | IndexOf(string, int, int, StringComparison) |
| Int32 LastIndexOf(Char) | LastIndexOf(char) |
| Int32 LastIndexOf(Char, Int32) | LastIndexOf(char, int) |
| Int32 LastIndexOf(Char, Int32, Int32) | LastIndexOf(char, int, int) |
| Int32 LastIndexOfAny(Char[]) | LastIndexOfAny(char[]) |
| Int32 LastIndexOfAny(Char[], Int32) | LastIndexOfAny(char[], int) |
| Int32 LastIndexOfAny(Char[], Int32, Int32) | LastIndexOfAny(char[], int, int) |
| Int32 LastIndexOf(System.String) | LastIndexOf(string) |
| Boolean IsNullOrEmpty(System.String) | IsNullOrEmpty(string) |
| Boolean IsNullOrWhiteSpace(System.String) | IsNullOrWhiteSpace(string) |
| Char& GetPinnableReference() | GetPinnableReference() |
| System.String ToString() | ToString() |
| System.String ToString(System.IFormatProvider) | ToString(IFormatProvider) |
| System.CharEnumerator GetEnumerator() | GetEnumerator() |
| System.Text.StringRuneEnumerator EnumerateRunes() | EnumerateRunes() |
| System.TypeCode GetTypeCode() | GetTypeCode() |
| Boolean IsNormalized() | IsNormalized() |
| Boolean IsNormalized(System.Text.NormalizationForm) | IsNormalized(NormalizationForm) |
| System.String Normalize() | Normalize() |
| System.String Normalize(System.Text.NormalizationForm) | Normalize(NormalizationForm) |
| Char get_Chars(Int32) | get_Chars(int) |
| Int32 get_Length() | get_Length() |
| System.String Concat(System.Object) | Concat(object) |
| System.String Concat(System.Object, System.Object) | Concat(object, object) |
| System.String Concat(System.Object, System.Object, System.Object) | Concat(object, object, object) |
| System.String Concat(System.Object[]) | Concat(object[]) |
| System.String Concat[T](System.Collections.Generic.IEnumerable`1[T]) | Concat(IEnumerable<T>) |
| System.String Concat(System.Collections.Generic.IEnumerable`1[System.String]) | Concat(IEnumerable<string>) |
| System.String Concat(System.String, System.String) | Concat(string, string) |
| System.String Concat(System.String, System.String, System.String) | Concat(string, string, string) |
| System.String Concat(System.String, System.String, System.String, System.String) | Concat(string, string, string, string) |
| System.String Concat(System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char]) | Concat(ReadOnlySpan<char>, ReadOnlySpan<char>) |
| System.String Concat(System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char]) | Concat(ReadOnlySpan<char>, ReadOnlySpan<char>, ReadOnlySpan<char>) |
| System.String Concat(System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char], System.ReadOnlySpan`1[System.Char]) | Concat(ReadOnlySpan<char>, ReadOnlySpan<char>, ReadOnlySpan<char>, ReadOnlySpan<char>) |
| System.String Concat(System.String[]) | Concat(string[]) |
| System.String Format(System.String, System.Object) | Format(string, object) |
| System.String Format(System.String, System.Object, System.Object) | Format(string, object, object) |
| System.String Format(System.String, System.Object, System.Object, System.Object) | Format(string, object, object, object) |
| System.String Format(System.String, System.Object[]) | Format(string, object[]) |
| System.String Format(System.IFormatProvider, System.String, System.Object) | Format(IFormatProvider, string, object) |
| System.String Format(System.IFormatProvider, System.String, System.Object, System.Object) | Format(IFormatProvider, string, object, object) |
| System.String Format(System.IFormatProvider, System.String, System.Object, System.Object, System.Object) | Format(IFormatProvider, string, object, object, object) |
| System.String Format(System.IFormatProvider, System.String, System.Object[]) | Format(IFormatProvider, string, object[]) |
| System.String Insert(Int32, System.String) | Insert(int, string) |
| System.String Join(Char, System.String[]) | Join(char, string[]) |
| System.String Join(System.String, System.String[]) | Join(string, string[]) |
| System.String Join(Char, System.String[], Int32, Int32) | Join(char, string[], int, int) |
| System.String Join(System.String, System.String[], Int32, Int32) | Join(string, string[], int, int) |
| System.String Join(System.String, System.Collections.Generic.IEnumerable`1[System.String]) | Join(string, IEnumerable<string>) |
| System.String Join(Char, System.Object[]) | Join(char, object[]) |
| System.String Join(System.String, System.Object[]) | Join(string, object[]) |
| System.String Join[T](Char, System.Collections.Generic.IEnumerable`1[T]) | Join(char, IEnumerable<T>) |
| System.String Join[T](System.String, System.Collections.Generic.IEnumerable`1[T]) | Join(string, IEnumerable<T>) |
| System.String PadLeft(Int32) | PadLeft(int) |
| System.String PadLeft(Int32, Char) | PadLeft(int, char) |
| System.String Intern(System.String) | Intern(string) |
| System.String IsInterned(System.String) | IsInterned(string) |
| Int32 Compare(System.String, System.String) | Compare(string, string) |
| Int32 Compare(System.String, System.String, Boolean) | Compare(string, string, bool) |
| Int32 Compare(System.String, System.String, System.StringComparison) | Compare(string, string, StringComparison) |
| Int32 Compare(System.String, System.String, System.Globalization.CultureInfo, System.Globalization.CompareOptions) | Compare(string, string, CultureInfo, CompareOptions) |
| Int32 Compare(System.String, System.String, Boolean, System.Globalization.CultureInfo) | Compare(string, string, bool, CultureInfo) |
| Int32 Compare(System.String, Int32, System.String, Int32, Int32) | Compare(string, int, string, int, int) |
| Int32 Compare(System.String, Int32, System.String, Int32, Int32, Boolean) | Compare(string, int, string, int, int, bool) |
| Int32 Compare(System.String, Int32, System.String, Int32, Int32, Boolean, System.Globalization.CultureInfo) | Compare(string, int, string, int, int, bool, CultureInfo) |
| Int32 Compare(System.String, Int32, System.String, Int32, Int32, System.Globalization.CultureInfo, System.Globalization.CompareOptions) | Compare(string, int, string, int, int, CultureInfo, CompareOptions) |
| Int32 Compare(System.String, Int32, System.String, Int32, Int32, System.StringComparison) | Compare(string, int, string, int, int, StringComparison) |
| Int32 CompareOrdinal(System.String, System.String) | CompareOrdinal(string, string) |
| Int32 CompareOrdinal(System.String, Int32, System.String, Int32, Int32) | CompareOrdinal(string, int, string, int, int) |
| Int32 CompareTo(System.Object) | CompareTo(object) |
| Int32 CompareTo(System.String) | CompareTo(string) |
| Boolean EndsWith(System.String) | EndsWith(string) |
| Boolean EndsWith(System.String, System.StringComparison) | EndsWith(string, StringComparison) |
| Boolean EndsWith(System.String, Boolean, System.Globalization.CultureInfo) | EndsWith(string, bool, CultureInfo) |
| Boolean EndsWith(Char) | EndsWith(char) |
| Boolean Equals(System.Object) | Equals(object) |
| Boolean Equals(System.String) | Equals(string) |
| Boolean Equals(System.String, System.StringComparison) | Equals(string, StringComparison) |
| Boolean Equals(System.String, System.String) | Equals(string, string) |
| Boolean Equals(System.String, System.String, System.StringComparison) | Equals(string, string, StringComparison) |
| Boolean op_Equality(System.String, System.String) | op_Equality(string, string) |
| Boolean op_Inequality(System.String, System.String) | op_Inequality(string, string) |
| Int32 GetHashCode() | GetHashCode() |
| Int32 GetHashCode(System.StringComparison) | GetHashCode(StringComparison) |
| Int32 GetHashCode(System.ReadOnlySpan`1[System.Char]) | GetHashCode(ReadOnlySpan<char>) |
| Int32 GetHashCode(System.ReadOnlySpan`1[System.Char], System.StringComparison) | GetHashCode(ReadOnlySpan<char>, StringComparison) |
| Boolean StartsWith(System.String) | StartsWith(string) |
| Boolean StartsWith(System.String, System.StringComparison) | StartsWith(string, StringComparison) |
| Boolean StartsWith(System.String, Boolean, System.Globalization.CultureInfo) | StartsWith(string, bool, CultureInfo) |
| Boolean StartsWith(Char) | StartsWith(char) |
| System.String Create[TState](Int32, TState, System.Buffers.SpanAction`2[System.Char,TState]) | Create(int, TState, SpanAction<char, TState>) |
| System.String Create(System.IFormatProvider, System.Runtime.CompilerServices.DefaultInterpolatedStringHandler ByRef) | Create(IFormatProvider, DefaultInterpolatedStringHandler&) |
| System.String Create(System.IFormatProvider, System.Span`1[System.Char], System.Runtime.CompilerServices.DefaultInterpolatedStringHandler ByRef) | Create(IFormatProvider, Span<char>, DefaultInterpolatedStringHandler&) |
| System.ReadOnlySpan`1[System.Char] op_Implicit(System.String) | op_Implicit(string) |
| System.Object Clone() | Clone() |
| System.String Copy(System.String) | Copy(string) |
| Void CopyTo(Int32, Char[], Int32, Int32) | CopyTo(int, char[], int, int) |
| Void CopyTo(System.Span`1[System.Char]) | CopyTo(Span<char>) |
| Boolean TryCopyTo(System.Span`1[System.Char]) | TryCopyTo(Span<char>) |
| Char[] ToCharArray() | ToCharArray() |
| Char[] ToCharArray(Int32, Int32) | ToCharArray(int, int) |
| System.Type GetType() | GetType() |

The following example table shows result of method execution on some other methods

| Method | Friendly name |
|--------|---------------|
| Table GenerateExampleTableForGetFriendlyMethodName(System.Reflection.MethodInfo[]) | GenerateExampleTableForGetFriendlyMethodName(MethodInfo[]) |

