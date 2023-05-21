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

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/X9J1Rjim38Rl0lGEFBh0d3Gz6aEGDYtGeDrI9CuksmwCpSH25ScGv7XzjXtsI7k5Ih63DSxS7WpB5Fdh-od2Vt__cLY7nbKxoPbz5gf4WpimP4kjB7qXQt5BDoHrVMsChi6PYZWh27CoS9-cJ_0qNwQ0fHZkHyFAah6SRRHsVZijX8LSu3P8saBN5bn1iAcaX19T0Ne3eG8oG-Y4LeCVAjLWYQ0kq461PKdAWg9V3cB82igU3mg7G2jiOHte1fmvuQHdMmM6gLREQ2d93AP1dA8BpaZbRSvQLof7ym8ttfFFvsm8oZE3tdkSDfZ0QWOPIYdK5cfQ1vFWDKejbFCkVQkSo4I9oa7dqDFDXY2aJFLkXog_zyzPLjXVKAcjSDgyS9QZmpLQWjjsyO7eWcmb7MSjVJpkC2Nm_TYAdxqtrwc9ntti2JIBw2p1Dxglg67wPp9vuo9wr5kXVF5fIZm-sazWzdLvjqZzAvqt3OTebd-2fX2-NJ-kxfOGJK9iSbJhG_aSnrsMyCtPOHQflxtJNczdEqr9uiUrxjEBGiHZOkR4_jrpEpVZ-Vj8tdRr_xFHcZyPZZ0Hmz5W15UNbp1_u6pYH_tmMtW5003__mC0)
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
database Database
participant CreateUserCommandResult
Browser->UsersController: [Create()] HTTP POST api/v1/users
UsersController->CreateUserCommandHandler: Handle(CreateUserCommand)
CreateUserCommandHandler->UserRepository: CreateUser(Login)
UserRepository->Database: INSERT INTO Users (Id, Login) VALUES (@Id, @Login)
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


