
# Business class

## Definition

Namespace: Synergy.Contracts.Requirements<br/>
Assembly: Synergy.Contracts.dll

Allows to create business requirement verification conditions and checks.

## Methods

| Name | Summary |
|------|---------|
| Rule(string) | Gets rule with description only. |
| When(bool, string) |  |
| Requires(bool) |  |

## Examples

### First step: business requirements gathering

Quite often you might hear during analysis phase of your project such words:

> When withdraw limit is set, withdrawn amount cannot exceed the limit

When you hear such sentence just start your development by writing it down like that:

``` csharp
Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
        .Throws(new NotImplementedException("NOT IMPLEMENTED"));
```

test