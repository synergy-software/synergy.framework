using System.CodeDom.Compiler;

namespace Synergy.Behaviours.Tests;

[GeneratedCode("Synergy.Behaviours.Testing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "1.0.0.0")]
public partial class CalculatorFeature
{


    [Xunit.Fact]
    public void AddTwoNumbers() =>  // Scenario: Add two numbers
        Given().TheFirstNumberIs50()  // Given the first number is 50
         .And().TheSecondNumberIs70()  // And the second number is 70
        .When().TheTwoNumbersAreAdded()  // When the two numbers are added
        .Then().TheResultShouldBe120() // Then the result should be 120
        ;

//  @Subtract
//  Scenario: Subtract two numbers
//    Given the first number is 50
//    And the second number is 25
//    When the two numbers are subtracted
//    Then the result should be 25
//
//  @Divide
//  Scenario: Divide two numbers
//    Given the first number is 100
//    And the second number is 2
//    When the two numbers are divided
//    Then the result should be 50
//
//  @Divide
//  Scenario: Divide by 0 returns 0
//    Given the first number is 0
//    And the second number is 70
//    When the two numbers are divided
//    Then the result should be 0
//
//  @Multiply
//  Scenario: Multiply two numbers
//    Given the first number is 5
//    And the second number is 50
//    When the two numbers are multiplied
//    Then the result should be 250
}
