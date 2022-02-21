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

This will throw `NotImplementedException` for sure. But we have not finished our job yet.

### Second step: make it working

When time comes you should implement the business requirement. It might look like that:

``` csharp
Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
        .When(withdrawLimit != null)
        .Requires(withdrawAmount <= withdrawLimit)
        .Throws($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})");
```

This sample is simplified for sure, but remember that simple solutions are the best.

### Third step: introduce dedicated exception

I like this step very much. Instead of using some generic exception thrown by `Throws()` method introduce your own meaningful exception:

``` csharp
Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
        .When(withdrawLimit != null)
        .Requires(withdrawAmount <= withdrawLimit)
        .Throws(new WithdrawAmountExceedsLimitException(withdrawLimit, withdrawAmount));
```

Introducing dedicated exception is not always needed. It allows you to handle (catch) some situations properly. It also gives you a way to derive all thrown business exceptions from your own base `Exception` class.

### Fourth step: document the requirement

TODO

``` csharp
Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
        .When(withdrawLimit != null)["Withdraw Limit is set"]
        .Requires(withdrawAmount <= withdrawLimit)["Withdraw Amount must be <= to Withdraw Limit"]
        .Throws(new WithdrawAmountExceedsLimitException(withdrawLimit, withdrawAmount));
```

### Fifth step: generate automatically markdown documentation

TODO: Describe how to convert the code to human readable markdown file using Unit Tests and ApprovalTests framework

### More samples

``` csharp
Business.Rule("Account balance cannot be less than 0")
        .Requires(balance >= 0)
        .Throws($"balance cannot be < 0 and actually is {balance}");
```

``` csharp
Business.Rule("For online payment transaction: when online payment limit is set than payment amount cannot exceed the limit")
        .When(transactionType == TransactionType.OnlinePayment)
        .And(onlinePaymentLimit != null)
        .Requires(paymentAmount <= onlinePaymentLimit)
        .Throws($"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})");
```

### Full example of value object with business requirements

TODO

