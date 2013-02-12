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



