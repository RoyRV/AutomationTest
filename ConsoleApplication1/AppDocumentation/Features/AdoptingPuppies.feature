Feature: Adopting puppies
  As a animal lover
  I want to adopt a puppie
  So I can make it happy
  
Background:
  Given I am on the puppy adoption site

@web  
Scenario: Thank you message should be displayed
  When I complete the adoption of a puppy
  Then I should see "Thank you for adopting a puppy!"