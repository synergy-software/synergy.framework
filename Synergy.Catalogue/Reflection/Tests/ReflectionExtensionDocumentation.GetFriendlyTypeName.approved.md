# Type.GetFriendlyTypeName() extension method

## Definition

Namespace: Synergy.Catalogue.Reflection<br/>
Assembly: Synergy.Catalogue.dll

Returns friendly name of Type.

## Examples

``` csharp
var type = typeof(string);
var friendlyName = type.GetFriendlyTypeName();
```

The following example table shows result of method execution on C# primitive types

| Input type | Friendly name |
|------------|---------------|
| System.Byte | byte |
| System.SByte | sbyte |
| System.Int16 | short |
| System.UInt16 | ushort |
| System.Int32 | int |
| System.UInt32 | uint |
| System.Int64 | long |
| System.UInt64 | ulong |
| System.Char | char |
| System.Single | float |
| System.Double | double |
| System.Decimal | decimal |
| System.Boolean | bool |

The following example table shows result of method execution on nullable types

| Input type | Friendly name |
|------------|---------------|
| System.Nullable`1[System.Byte] | byte? |
| System.Nullable`1[System.SByte] | sbyte? |
| System.Nullable`1[System.Int16] | short? |
| System.Nullable`1[System.UInt16] | ushort? |
| System.Nullable`1[System.Int32] | int? |
| System.Nullable`1[System.UInt32] | uint? |
| System.Nullable`1[System.Int64] | long? |
| System.Nullable`1[System.UInt64] | ulong? |
| System.Nullable`1[System.Char] | char? |
| System.Nullable`1[System.Single] | float? |
| System.Nullable`1[System.Double] | double? |
| System.Nullable`1[System.Decimal] | decimal? |
| System.Nullable`1[System.Boolean] | bool? |

The following example table shows result of method execution on some more complex types

| Input type | Friendly name |
|------------|---------------|
| System.Object | object |
| System.String | string |
| System.DateTime | DateTime |
| System.DateTimeOffset | DateTimeOffset |
| System.Collections.Generic.List`1[System.Int32] | List<int> |
| System.Int32[] | int[] |
| System.Collections.Generic.Dictionary`2[System.String,System.Int64] | Dictionary<string, long> |

## Remarks

This method is intended to be used mainly for testing purposes.

