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

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/Z5JDRfj04BuZyGvpf5AnunwjAdBYMehLj8vit4kQmnh6PXMyYtO7KpzR3tsala9d0QinX3W7n38_ttppxGn_V_-PM1A6YdtcU_PPgbmOiGU3DjVAuZUqLkpm5ZDTtXYZIo1JeE-bA18qS1T53_2mM4KWSZayZ8Q5HMDzRwiriJjAfOL4Yfs3jAakBL2Ai2so3791AUWjk0o83GgIMWr-gaY3HOGo5GIfo7DK5XJ-8WWXJZ5-hX0gGmDiOUFO3NoF96NCRUquJBKYexCCpM3gm37uu7keaYPcemkL27E4M-w9utrl28evWpumdSOuXlKSOf5bKksWn8rh4XWZrr8HTybIaOnbBXH1g-YvisRWGgPwlnSgkUEd4-NyIyorbQJDiUlplKIGs0YByAKvN2YpH5jav7jDF-5rY-KO7a_YFFNUPIidlExhPWpr8UW4iFHzIJMd_ssFNtGHtEkTL9miOf87DdOaEl_kOiqL3xSYsG3E-4Z7ox3LEDRcrskCUGBBoXzOCZmU7-5LKBuoV3VvqwKoxU-hsJBYLxI0nqh_9mZcoQa8_BYvNyzM44oSRNAg_4QfFlK_Xs5Riffseq3lHFLVPqzAhLYFCt2MbzhrrcqD8_FldyPBwz0qVxOFRWc6ey48FbrTmUAhxqrutwj_upy0003__mC0)
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
UserRepository->Database: INSERT INTO [Users] (Id, Login) VALUES (@Id, @Login)
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

![Sequence Diagram for UsersController.GetUsers()](http://www.plantuml.com/plantuml/png/V5DBRi8m4DrRyXrcIIk5JvTH5V4fXQf5j6bO8HQ6ZAXLOqUsGvIpTT4ZzGfr81Gb3Unip_EypzW_NzzzOvcswKvGOZwvJ9XcEz1e4YKDpj0OjiKX2fKDj5OPM9qY9H_8OjGmZQ9Nc8mZO0dlx7kTrA0sb5XkXGCjYjr8IQkL4AZR4xI7ezOD9IZZ4hLMgOoPpc7eshio9HsGoYAelMCe3trOFC662S7b5Z9S5uJWUYIAI-iOdMhBDpnXqaADjbeyQNXBKUTJ9kD_YA8SOg8CjqhdbCJCiZKp20_buah344qgB2MbQY-eQV5XUGvXTIc-sYKlQ1Bjmt7HgXHTiatWev2pAx-GFH3YRnpLiXUSlFhmFduPZoAuXSTmFeFbuSQgcUNUy-eqeNidSodoKU5aOxcIzmKae6IZaRaXDyRNRA41weF4hAb7wnAnyyhSTd-HknQ-yzIS_hMvbq5K1b_CkjDhz-2ksuNvCoLzztmFl-uN003__mC0)
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
participant GetUsersQueryHandler
participant UserRepository
database Database
participant GetUsersQueryResult
Browser->UsersController: [GetUsers()] HTTP GET api/v1/users
UsersController->GetUsersQueryHandler: Handle(GetUsersQuery)
GetUsersQueryHandler->UserRepository: GetAllUsers()
UserRepository->Database: SELECT * FROM [Users]
GetUsersQueryHandler<--UserRepository: ReadOnlyCollection<User>
create GetUsersQueryResult
GetUsersQueryHandler->GetUsersQueryResult: new GetUsersQueryResult(ReadOnlyCollection<User>)
activate GetUsersQueryResult
UsersController<--GetUsersQueryHandler: GetUsersQueryResult
Browser<--UsersController: HTTP/1.1 200 OK
@enduml
-->


##  UsersController.GetUser(string)

**Root type:** `UsersController` (from: `Synergy.Sample.Web.API`)

**Root method:**
```
    GetUser(
       userId: string
    ) : GetUserQueryResult
```

![Sequence Diagram for UsersController.GetUser()](http://www.plantuml.com/plantuml/png/X5JBJiCm4BnRyZ_iiHnAACUegaghyX22GjMU40UJB61XxCZUj4I8B-F09_4Bs6q4QN3Xb6GVdjcPZJ_VFuQEXANoMN7cdgGkX1NFOD4LHZkyGEV40uvGcTMHjMO5P4lax155ZXPEv_ChE9dCGHGoMVQIqg5roMjudELld94avQiN8JmscgnH2kt-2L88TVOuGvtNHNUcrBcm5OmyZCzobe0sX62M7gaEfh0uWqme9VK3hF0k08C_ep1Ia-FCioUPoK9eWXRgThAcS5sYhKw5pdyLXEmC2-Ca6LjjvsReJ6apl3Hq75XFNZ8iI1eTnN1h4DzJAkAi7gGxQD5BuUPRbjj_XMqrTmUHULBOl7IQEIzvf7J3vcVS58wbpaDiL7KSMQzslB7VxROxmpTdGX78UmY4Wujm7tJYBBCelAL_21XbjxC-1NeCMwbnjVlGdmb4HdBv3m5iy8mw66KOgKpNh2A9pip_FLEjgd4mprCokXzg1as6KUYMxLxzgE_DO6llQiEs5YyiMzBRxy7XmG5CppaRUWtM5y8N0000__y30000)
<!--
@startuml
skinparam responseMessageBelowArrow true
header HTTP GET api/v1/users/{userId}
title
UsersController.GetUser()
endtitle
boundary Browser
/ note over Browser: UI calling web api endpoints
participant UsersController
participant GetUserQueryHandler
participant UserRepository
participant ResourceNotFoundException
participant GetUsersQueryResult
Browser->UsersController: [GetUser()] HTTP GET api/v1/users/{userId}
UsersController->GetUserQueryHandler: Handle(GetUserQuery)
GetUserQueryHandler->UserRepository: FindUserBy(string)
GetUserQueryHandler<--UserRepository: User
alt if user not found
create ResourceNotFoundException
GetUserQueryHandler->ResourceNotFoundException: throw new ResourceNotFoundException(string)
activate ResourceNotFoundException
end
create GetUsersQueryResult
GetUserQueryHandler->GetUsersQueryResult: new GetUsersQueryResult(ReadOnlyCollection<User>)
activate GetUsersQueryResult
UsersController<--GetUserQueryHandler: GetUserQueryResult
Browser<--UsersController: HTTP/1.1 200 OK
@enduml
-->


