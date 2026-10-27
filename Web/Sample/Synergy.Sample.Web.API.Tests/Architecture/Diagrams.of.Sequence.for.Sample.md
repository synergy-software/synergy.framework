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

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/Z5HDSzem4BthL-pHpDGGUlHqCYIKcJ1DImPCBcaEmbwm9aBoI6jSVbiF_Kdz2vLiCmqcQdhmMEp7sxTFp_p--MjYYHkgzfBP5w5ARlWU3DfIAujVqLg-mrkKkhunHjT0fa9M8C_Hm5sQFi33OfK2ByNeC1vL5erbMwt9PTD2MCW5ttb0M-ZQ0XK8sqfAA3aLeBVW6o0po4beDVokKWqM4Ug24nIyB55PKFY388QimEobGMW27R25ZUSsP2H88bjx0bEjo6WfqGod7XgZ0KELjnKRNQcScoFSkcrSDHk1SdH17noJBfR0UWuPbrAe7TIuyUk1GoYrK6IPauX49agk27e3ptBjS5ynrVizL_cTU_f5Fht4Kbj1sXmlKYpdn3VS8dpk3lyUiKHRIMBT8l5rZry2JoT9daFNrsk9hqDx9D0Ueek20Glrj8p-BfoyMY6wrpkX1end90ukrjVc_ESbqbmv9oiI7TpC7Udu7cXZlZRwve34sQrkqf4bumpn0Tv2P3d-B-tJHJh2trQpPUfUwGAU6jcV8PhdfmdmU7E_dgqWclZOf1iR7XJI_5CSzwLgA7UhXpmKli90HojL81dvo2lHGbDxxdFSm_Pxn_tTtcVszvuVZOTZ-7Xr1OilRE8-Q__NzmS00F__0m00)
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

![Sequence Diagram for UsersController.GetUsers()](http://www.plantuml.com/plantuml/png/V5D1Ri8m4Bpx5Nk4IY7GOuIY0gLGjOWs3IV4mIGhQjNOaUqGvMqzz4dzGXq8GecStZQUySpihlFx_VEW3LKcFtAYlvZ8gA97KAWpAJIkK6jwm2boMKoKaWKObIFvH9gYWcKSly5Y7WFDc7yQ-Rb6fObXXYFPLFLC2gCavwW62pJderwVe4WldBtCHKfL2LEhR47YWv06GPwiU7qMmEOP4iev4mSeS5zvWLN891D64nlNi8HbL1XeMJQmg_rxZgfSKf6s2HKQOIOrCrAL9AM6xgb6UAoBRhK8TSuDgVDwOIj60DjRxxkEcRNkUA4hRm2NejS0-yJ5lSIuDHHKcIUSNtVGHBtmscO07_FN-Io6EtYArYlOdY_id1vZpskRHFPXh0KlPrKRYM5IZ2jAI1A5r6xMDJXtV0Sp0865IwBNPTidr7wTkfnRKxVzkCV-pxhh6JJsNQtO7mr6S3ySmlg5FDZdMlrUVm000F__0m00)
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

![Sequence Diagram for UsersController.GetUser()](http://www.plantuml.com/plantuml/png/X5DDJm8n4BttLymH3h3WSKC8GlYAKPJWoNWekmCqbdRJpe8Ruo_pu4_oBzW28coAdhOxRwRlpRpfryTdnn8tbAybioz2PTpmDHYqcLOMRz5QliGkIhszDaPlWKoER8KyHGEZsUmUXlqPy4n4csQKMpGsUlEVSVhEI914zkY3FQt8Q2dHr8T8FbIfCbJfFcMkSvLoKq3NCJYCHQ0q8UYD8pd4OdWSGyAb56e9MvnxJd0tP5eeiip99f68Z2k24kCPTc1_oD4K8wxICkx1AMRQ2jAcE8EcQ7Lk4hpJDF22-wy9PYIq2X7O7OChoIMnGmkrTabP34_7QJp_DyrIRQqTQ2M6_Q5oYbLP87ElvRVJ60P2fJxMBIgMZ9joiAvLgvKB_J_Zaa0im8lrti72JuWb1hdpyVBaWiekfiT0Ax-32hUNxpogvmc9pT_qRdz-D8Py2weB9COxHG6WCdKFPQ9aqVEUEJrQjNnE-qHTYBZajHjwqEpJuFcg7MmwspM_Nr6pteIhHWCcDwpZklUl_Xi00F__0m00)
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


