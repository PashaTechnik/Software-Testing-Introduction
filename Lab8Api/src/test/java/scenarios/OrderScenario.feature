@ApiTests
Feature: Api testing with help of Cucumber

  Scenario: Get forecast by city
    Given city is "Kiev"
    When user try to get forecast by city name
    Then user receive status code 200
    And response don't equal zero


  Scenario: Get forecast by city FAIL
    Given city is "emvmlfkvmlkef"
    When user try to get forecast by city name
    Then user receive status code 200
    And response don't equal zero
