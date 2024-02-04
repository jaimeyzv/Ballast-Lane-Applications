# WEB API Project
## Introduction

This is a web api application built based on Clean Architecure which allow us to separate the code into its different responsibilities. this demo follows specifications from the following user story detailed below:

## User Story 001
As a user, I want to READ all user registered in the system so that I can review them and see who they are.
### Acceptance Criteria
**Scenario 1**
Given that user needs to see all users registered
When the user goes to "See all users" page 
Then user will see a list of all users

## User Story 002
As a user, I want to LOOK UP for a particular user registered in the system so that I can review it and see who that user is.
### Acceptance Criteria
**Scenario 1**
Given that user needs to find a particular user
When the user goes to search page and filter by user identifier
Then user will see the found user and its information.

**Scenario 2**
Given that user needs to find a particular user
When the user goes to search page and filter by a wrong user identifier or non-existing user identifier
Then user will see in the page this message "User with identifier [IDENTIFIER] was not found."

## User Story 003
As a user, I want to CREATE a new user so that I can get that new user registered in the system.
### Acceptance Criteria
**Scenario 1**
Given that user needs to create a new user
When the user goes to "New User" page 
And clicks on Save button
Then the user will be created and get the message "User [NAME] was successfully created"

## User Story 004
As a user, I want to UPDATE information of a particular user in the system so that I can get that user with lastes data.
### Acceptance Criteria
**Scenario 1**
Given that user needs to update a particular user
When the user goes to the edit page and insert new values on the fields Ane clicks on Save button
Then user will see the user infomation with updated data and see the message "User [NAME] was successfully updated"

**Scenario 2**
Given that user needs to update a particular user
When the user goes to the edit page and insert new values on the fields Ane clicks on Save button AND the user identifier is wrong or non-existing user identifier
Then user will see the message "User with id [IDENTIFIER] does not exist. Resource cannot be updated"

## User Story 005
As a user, I want to DELETE a particular user so that I can get system updated withput that removed user.
### Acceptance Criteria
**Scenario 1**
Given that user needs to delete a particular user
When the user goes to the list page and clicks on delete icon
Then user will see the list page updated without that removed user

**Scenario 2**
Given that user needs to delete a particular user
When the user goes to the list page and clicks on delete icon And the user identifier is wrong or non-existing user identifier
Then user will see the message "User with id [IDENTIFIER] does not exist. Resource cannot be deleted"
