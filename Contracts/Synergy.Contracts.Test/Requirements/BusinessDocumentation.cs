using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Synergy.Contracts.Requirements;
using Synergy.Documentation.Api;
using Synergy.Documentation.Code;
using Synergy.Markdowns;
using VerifyXunit;
using Xunit;

namespace Synergy.Contracts.Test.Requirements
{
    [UsesVerify]
    public class BusinessDocumentation : BusinessTest
    {
        [Fact]
        public async Task Generate()
        {
            // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: check that and probably convert docs int tt

            BusinessUsage docs = new BusinessUsage();
            var content = docs.TransformText();

            await Verifier.Verify(content, "md");
        }
        
        public static string? Read(string method)
        {
            return ClassReader.ReadMethodBody(method);
        }
        
        [Fact]
        public void General()
        {
            var documentation = new Markdown.Document();

            documentation.Append(new ClassDocumentation(typeof(Business)));

            documentation.Append(new Markdown.Header2("Examples"))
                         .Append(this.Step1GatherRequirements())
                         .Append(this.Step2MakeItWorking())
                         .Append(this.Step3IntroduceDedicatedException())
                         .Append(this.Step4DocumentTheRequirement())
                         .Append(this.Step5GenerateDocumentation())
                         .Append(this.QuickSamples())
                         .Append(this.ValueObjectExample())
                ;

            Verifier.Verify(documentation.ToString());
        }

        private void Step1Sample()
        {
            Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
                    .Throws(new NotImplementedException("NOT IMPLEMENTED"));
        }

        private IEnumerable<Markdown.IElement> Step1GatherRequirements()
        {
            yield return new Markdown.Header3("First step: business requirements gathering");
            yield return new Markdown.Paragraph("Quite often you might hear during analysis phase of your project such words:");
            yield return new Markdown.Quote("When withdraw limit is set, withdrawn amount cannot exceed the limit");

            yield return new Markdown.Paragraph("When you hear such sentence just start your development by writing it down like that:");

            this.Act(() =>
                    this.Step1Sample()
                )
                .AssertException("NOT IMPLEMENTED");

            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.Step1Sample)));

            yield return new Markdown.Paragraph("This will throw `NotImplementedException` for sure. But we have not finished our job yet.");
        }

        private void Step2Sample(int withdrawLimit, int withdrawAmount)
        {
            Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
                    .When(withdrawLimit != null)
                    .Requires(withdrawAmount <= withdrawLimit)
                    .Throws($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})");
        }

        private IEnumerable<Markdown.IElement> Step2MakeItWorking()
        {
            yield return new Markdown.Header3("Second step: make it working");
            yield return new Markdown.Paragraph("When time comes you should implement the business requirement. It might look like that:");

            var withdrawLimit = 100;
            var withdrawAmount = 110;

            this.Act(() =>
                    this.Step2Sample(withdrawLimit, withdrawAmount)
                )
                .AssertException($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})");

            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.Step2Sample)));
            yield return new Markdown.Paragraph("This sample is simplified for sure, but remember that simple solutions are the best.");
        }

        private void Step3Sample(int withdrawLimit, int withdrawAmount)
        {
            Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
                    .When(withdrawLimit != null)
                    .Requires(withdrawAmount <= withdrawLimit)
                    .Throws(new WithdrawAmountExceedsLimitException(withdrawLimit, withdrawAmount));
        }

        private IEnumerable<Markdown.IElement> Step3IntroduceDedicatedException()
        {
            yield return new Markdown.Header3("Third step: introduce dedicated exception");
            yield return new Markdown.Paragraph($"I like this step very much. Instead of using some generic exception thrown by" +
                                                $" `{nameof(Business.Requirement.Throws)}()` method introduce your own meaningful exception:");

            var withdrawLimit = 100;
            var withdrawAmount = 110;

            this.Act(() =>
                    this.Step3Sample(withdrawLimit, withdrawAmount)
                )
                .AssertException($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})");

            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.Step3Sample)));
            yield return new Markdown.Paragraph("Introducing dedicated exception is not always needed. " +
                                                "It allows you to handle (catch) some situations properly. " +
                                                "It also gives you a way to derive all thrown business exceptions from your own base `Exception` class.");
        }

        private void Step4Sample(int withdrawLimit, int withdrawAmount)
        {
            Business.Rule("When withdraw limit is set, withdrawn amount cannot exceed the limit")
                    .When(withdrawLimit != null)["Withdraw Limit is set"]
                    .Requires(withdrawAmount <= withdrawLimit)["Withdraw Amount must be <= to Withdraw Limit"]
                    .Throws(new WithdrawAmountExceedsLimitException(withdrawLimit, withdrawAmount));
        }
        
        private IEnumerable<Markdown.IElement> Step4DocumentTheRequirement()
        {
            yield return new Markdown.Header3("Fourth step: document the requirement");
            yield return new Markdown.Paragraph("TODO");
            
            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.Step4Sample)));
        }

        private IEnumerable<Markdown.IElement> Step5GenerateDocumentation()
        {
            yield return new Markdown.Header3("Fifth step: generate automatically markdown documentation");
            yield return new Markdown.Paragraph("TODO: Describe how to convert the code to human readable markdown file using Unit Tests and ApprovalTests framework");
        }
        
        private IEnumerable<Markdown.IElement> QuickSamples()
        {
            yield return new Markdown.Header3("More samples");
            
            foreach (var element in this.Sample1().Union(this.Sample2()))
                yield return element;
        }
        
        private IEnumerable<Markdown.IElement> Sample1()
        {
            var balance = -10;
            this.Act(() =>
                    this.QuickSample1(balance)
                )
                .AssertException($"balance cannot be < 0 and actually is {balance}");

            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.QuickSample1)));
        }

        private void QuickSample1(int balance)
        {
            Business.Rule("Account balance cannot be less than 0")
                    .Requires(balance >= 0)
                    .Throws($"balance cannot be < 0 and actually is {balance}");
        }

        private IEnumerable<Markdown.IElement> Sample2()
        {
            TransactionType transactionType = TransactionType.OnlinePayment;
            double? onlinePaymentLimit = 10;
            double paymentAmount = 11;

            this.Act(() =>
                    this.QuickSample2(transactionType, onlinePaymentLimit, paymentAmount)
                )
                .AssertException($"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})");

            yield return new Markdown.Code(ClassReader.ReadMethodBody(nameof(this.QuickSample2)));
        }
        
        private void QuickSample2(TransactionType transactionType, double? onlinePaymentLimit, double paymentAmount)
        {
            Business.Rule("For online payment transaction: when online payment limit is set than payment amount cannot exceed the limit")
                    .When(transactionType == TransactionType.OnlinePayment)
                    .And(onlinePaymentLimit != null)
                    .Requires(paymentAmount <= onlinePaymentLimit)
                    .Throws($"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})");
        }

        private IEnumerable<Markdown.IElement> ValueObjectExample()
        {
            yield return new Markdown.Header3("Full example of value object with business requirements");
            yield return new Markdown.Paragraph("TODO");
        }
        
        private class WithdrawAmountExceedsLimitException : Exception
        {
            public WithdrawAmountExceedsLimitException(int withdrawLimit, int withdrawAmount) : base(
                $"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})")
            {
            }
        }
    }
}