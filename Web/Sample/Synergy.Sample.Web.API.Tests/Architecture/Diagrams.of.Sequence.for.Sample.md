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

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/Z5HDRzf04BrRyd-OeolL47gqgeY48WKrBH6ONfeS5dl0gvXTQtUCos_heJ-fVw6pjb41uwO7n38VRzvxEylldx_6beIXSfVxddsLgX16xC2WBRIo-0Mj5LkynrnNTyRe2iYKw7iPYXGDFCJn4ppDbp68GWxtes5fqLZVssXDd8upQI6LOkiWRQOh2vGXRCeyXq9G1de3hWCIWuAaLeDd5MkmY51bWY0JHO7AWi8V124a6IQlDK8TQ84jh1sxWU-Hf9ovhHo7YLPaT9wZ6KmSE0RlV0zLsjQiTQbIOGvmpvgutlU6e9Wpw3tJQOCHh6QGY3oNQWiLhfr8O8n2IqMiagqYcSX2A8ABeUV9Xe4hcUZTJgZqWJ-TAfTVOA6j96qEtPplfOB4MbY4J-tXZJ4Bj6LEljVg2MylM4Rm_MZEI-zTNlI4jtrg8cWEGQU0hUzhQZZzbHsTg0WUzLOgRXO9oJq7EnQT_-vYpHGljoBP0axvI8UtOPEQGclwjHaHRs1LvmDBXjVZFLm5vIl3_oP_l5Ic_NKvNSJy5S-RnO9WbXv7mBUxnzLq2S7OnSR7kVyOrEVznp2yDAmXtUhltQT9xsLsCarBurTFCd2HK-TwwZghoFpxc9okqVN7q8e_Umtk2Gn7Wn5ykBc1-MVV6_Dhh_yP_m000F__0m00)
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


