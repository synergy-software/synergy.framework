@tag
Feature: Calculator

  ![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
  Simple calculator for performing calculations on **two** numbers

  Background:
    Given User opened calculator

  Rule: Adding numbers

    @Add @Verify
    Scenario: Add two numbers
      Given the first number is 50
      And the second number is 70
      When the two numbers are added
      Then the result should be 120

    @Add
    Example: Add two numbers in "different" way
      Given Two numbers:
      * the first number is 50
      * the second number is 70
      When the two numbers are added
      Then the result should be 120

    @Add
    Scenario Outline: Add many numbers
      Given the first number is <first no>
      And the second number is <second>
      When the two numbers are added
      Then the result should be <result>
      Examples:
        | first no | second | result |
        | 0        | 70     | 70     |
        | 50       | 70     | 120    |

    @Subtracting
    Rule: Subtracting numbers

    # tests os subtracting numbers

    @Subtract
    Example: Subtract two numbers
      Given the first number is 50
      And the second number is 25
      When the two numbers are subtracted
      Then the result should be 25

    @Subtract
    Scenario Template: : Subtract two numbers
      Given the first number is <first>
      And the second number is <second>
      When the two numbers are subtracted
      Then the result should be <result>
        Examples:
        | first | second | result |
        | 0     | 70     | 70     |

  Rule: Dividing numbers

    @Divide
    Scenario: Divide two numbers
      Given the first number is 100
      And the second number is 2
      When the two numbers are divided
      Then the result should be 50

    @Divide
    Scenario: Divide by 0 returns 0
      Given the first number is 0
      And the second number is 70
      When the two numbers are divided
      Then the result should be 0

  Rule: Multiplying numbers

    @Multiply
    Scenario: Multiply two numbers
      Given the first number is 5
      And the second number is 50
      When the two numbers are multiplied
      Then the result should be 250

    @Multiply
    Scenario: Multiply two numbers and send the result
      Given the first number is 5
      And the second number is 50
      When the two numbers are multiplied
      Then the result should be 250
      And the result should be sent to the email test@results.com
      