# Sequence diagrams for Sample Web API management

##  UsersController.Create(string, CreateUserCommand)

**Root type:** `UsersController` (from: `Synergy.Sample.Web.API`)

**Root method:**
```
    Create(
       version: string,
       user: CreateUserCommand
    ) : ActionResult<CreateUserCommandResult>
```

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/Z5JDRfj04BuZyGvpf5AnunvH5JbnBSLgMaSsxgNjOGrZimhUHRk3gP-jXpvINw6pWDKOGjq3OfcVRxvlTeRVFtzDBGb3vI7tFViiLI6CE811MsXbyHDQA_PuZxckxepH5P0fqVSo52aQU8ZZ9tXQhcCGXHmV9-FIeh6-jzEQs1rdqa8gnTv1sanN5YX3s9Lv3eMW3FGEN0Oa1WL9hKRVLAp18aAL2O9C50Kg2mf_48IGP9WyrmYreGMsi7NiHhv7adBcjd4SPbgHqNcEPZHpu1YyyJrKQHkprQLAXJd1FMlYUDyRWsBEe8zCfpL6i5b08l9SgZrKk7KYWJ4ABHMnIcuLoKGMGX5qYbuw6mOkPAOF1w7I1tvwKSw_ma9RITgS-ZxVImM9hR087zh3bJ8hj6LEljVg2MyxB2FuUcxEzy6xxEI4jqDg8cWEGI-0Mp-Kr73wApjweI9ur7kfE5aa98zixBNeyhkFjL0yt8fa2pZd8vskmzRZs9XVhn7n15QrFx1aU3pUmgkWV6Nut-JFbygqFw_dgvXVyR8PB0WMwRa4VBbxtCpN44oTRNgk-uz2Gxr_7uRTXZMaM_s3ypHycGCfJRy6d86plEpSODtEA3B_uLcyjWoj-8jjS2imdemcyExc1fOVVM_Asrx_6Vy0003__mC0)
<!--
@startuml
skinparam responseMessageBelowArrow true
header HTTP POST api/v1/users
footer This diagram shows the full path of user creation.\nTo see what happens next - check the next diagrams below.
title
UsersController.Create()
endtitle
boundary Browser
/ note over Browser: UI calling web api endpoints
participant UsersController
participant CreateUserCommandHandler
participant UserRepository
participant User
database Database
participant CreateUserCommandResult
Browser->UsersController: [Create()] HTTP POST api/v1/users
UsersController->CreateUserCommandHandler: Handle(CreateUserCommand)
CreateUserCommandHandler->UserRepository: CreateUser(Login)
activate UserRepository
UserRepository->UserRepository: InstantiateUserEntity(Login)
activate UserRepository
create User
UserRepository->User: new User(string, Login)
activate User
deactivate UserRepository
UserRepository->Database: INSERT INTO Users (Id, Login) VALUES (@Id, @Login)
deactivate UserRepository
CreateUserCommandHandler<--UserRepository: User
create CreateUserCommandResult
CreateUserCommandHandler->CreateUserCommandResult: new CreateUserCommandResult(User)
activate CreateUserCommandResult
UsersController<--CreateUserCommandHandler: CreateUserCommandResult
Browser<--UsersController: HTTP/1.1 200 OK
@enduml
-->

This diagram shows the full path of user creation.
To see what happens next - check the next diagrams below.


##  UsersController.GetUsers()

**Root type:** `UsersController` (from: `Synergy.Sample.Web.API`)

**Root method:**
```
    GetUsers() : GetUsersQueryResult
```

![Sequence Diagram for UsersController.GetUsers()](http://www.plantuml.com/plantuml/png/T53DIWD13BuFp3lag8VjTdii8hKYRIbY3zkJU4YxeGvECqCcsyLdy-0ZzHMSBGj2nME-V7_9-UjxbXH5swEt9dquZYXu1A4K0oTwffJmG7FoeNiG2Hsej6JDEs530ikwti3YgGQChZnLPPj8aZNgr6VIjfyU0wi4xqb62z8BT7Dh3N4pi7QXvGRb4-RPFg-jAO634eHJJXZ0AMnNi4Vl7H-WerqV2DaZ1iUQ4tDhTNiNaHMkOgqPF8hxgyqKNdyhlVrtpA2-Aue_ybvHLgCA9kCnlAojcULAbq_-0000__y30000)
<!--
@startuml
skinparam responseMessageBelowArrow true
header HTTP GET api/v1/users
title
UsersController.GetUsers()
endtitle
boundary Browser
/ note over Browser: UI calling web api endpoints
participant UsersController
Browser->UsersController: [GetUsers()] HTTP GET api/v1/users
Browser<--UsersController: HTTP/1.1 200 OK
@enduml
-->


