LivingDocumentation
===================

Tools for creating Living Documentation from Specifications by Example - http://specificationbyexample.com/

Currently only supporting Java/JBehave (.net/SpecFlow coming) and Target Process

Create features and scenarios, tag them with matching entity id values from Target Process, automatically export the results of the test



The Problem
===========
Different members of the team (product owners, developers, QA) all have different ideas of what the application is supposed to do, and how.

Documentation, if it even exists, is vague and out of date.

Unit tests are only accessible and readable by developers

Nobody can definitively say what the application is actually capable of, right now

A Solution
==========
Stakeholders (product owners, developers, QA) collaborate to produce representive examples of application requirements.

Developers then encode these into a BDD framework (i.e. JBehave right now), and run them against the live code

Results then get written to an accessible project management system (i.e. Target Process right now)

Product Owners can get an accurate, readable view of the project

Developers know if they're done

QA focus on new, more cunning exploratory tests, with the routine work automated


JBehave Solution
================
This is my first and only Java project, so it's not perfect

Create a free Target Process account - http://www.targetprocess.com/pricing/

Set up an account with developer priviliges

Create a user story, and attach test cases

Using the downloaded JBehave code as a model, annotate the JBehave stories with the matching id values from Target Process:

Rename \src\main\resources\livingDoc.properties.rename to livingDoc.properties, and update with your Target Process url, username and password

Run the stories!

The results will be written to Target Process


Sample Story
Basic maths

Meta:
@UserStory 224

Narrative: 
In order to know maths
As a counter
I want to add and subtract

Scenario:  Subtraction
Meta:
@TestCase 225

Given I enter <n1>
And I enter <n2>
When I add
Then I should get <sum>

Examples:
|n1|n2|sum|
|30|-20|10|
|30|-5|25|



Scenario:  BigNumbers
Meta:
@TestCase 226

Given I enter <n1>
And I enter <n2>
When I add
Then I should get <sum>

Examples:
|n1|n2|sum|
|30000|50000|10|



Scenario:  Addition
Meta:
@TestCase 227

Given I enter <n1>
And I enter <n2>
When I add
Then I should get <sum>

Examples:
|n1|n2|sum|
|30|10|40|




