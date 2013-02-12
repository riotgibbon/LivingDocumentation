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


