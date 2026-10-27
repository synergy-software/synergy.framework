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

![Sequence Diagram for UsersController.Create()](http://www.plantuml.com/plantuml/png/Z5HDRzf04BtlhvYZaMe8FLfHH4AHWfg6Y9XSqXm6Uy2hc5bhTupBR-kXFwb_eLhRgC74JM_h-NXltfkHV__yDN62LifThjohvW8jxi2IAmmx-aREuPPkA3VLjRMc0h4bgOmm9GktSVm03ul76B3Ge_ruL3goJcsC4R8GPzf1gd7h0LrcAWUI4MpAF8S29GEp0Ty0YIKKRNZudMC3ZWYg30KoB0fY1qm_1498CafUQuGwq08xMFlPXagqvAHMVe2fOR4cpyaEfnwQWe4YJfkAjIavHNk06sigHrQDW8qGc3tPOoo2rHmIp7FDMwXexUK1SLeOpU9KWLPqeWjaWGxXIQuXznLJiziXfxV8QRV8fvTK6AV5sCDPIgKek4P7yALz_9jYIQxCHRL2mglEV14y7orvwLjVfoMywjCHGVC8pWe6gg-dcUYluEYDXE3ER3KF52QYzoZKzURqynnfpawGHRTmCnOjXuz0avg-yVezWWYOgZeTEB6Qjv_WFKILql-EVLna1FFxnzaoXlbzl83dslON2ERfaG6UhkzMiqS89ZusQMdxYVeylmp3hbNroAtqQSyDzQ-mfwEngYSP-CWRq_fOEzTt6ORzv_V1zRUwJyxVt_neF1p3vuiBM7nL4-BK__h-0000__y30000)
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

![Sequence Diagram for UsersController.GetUsers()](http://www.plantuml.com/plantuml/png/V5DBRi903DtFARo4Ik7JPOGGdrAeMaHBmmgnC8b5Hnrc8ez3bBDrqIFr2bKA2165xYpxUTx7cf-lxu4NPCdsHlbFRLDat0EJJvtrD2VlSKSZCYuVChiSX3DI7uG9CSoYw1McamWmrUr3jvrvOgz4Yo6rAkkniyBE6EBMbEIlrMWgiiaHitMPJP0B6B7BFR5gWtL2u0x4vru8goU8qHXjTv3JjkG2iadgj1MlKcJHiKxH2bGehsPd-hUCk9YXJQg0Shgar7ajZWkLeE0MFS73gRZzsf9yPaITz0RzYemGrXVlcnkPLNQ2VftU48v5ushOL7NOeuoBeR3KF3JcV8FhQT0_smpXVV8o6KTm1u_BnHpMVmkRMevU45H9beJ9mffYNDg8HJlRAo5z5JEXK6rmzV9ha25OokkUQDoYRIgCHHzkCLTIxmL1VUp_dFkKmTMzonEtkwqkt7Swi7XM0x99-Rr-0G00__y30000)
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

![Sequence Diagram for UsersController.GetUser()](http://www.plantuml.com/plantuml/png/X5DDJnj13BtFh_0n79Ob75THH8Fu4WBQA9nG3yEE2IC6pyhZpNPLzPVrq9_Klr2Pf92i1ZZjh9yzxzdFy_VtdwCabgLz3YOzUMeissTWJ4saX5UOabtY34FilZ373eHRD8ze7JASBnPVuUna0RRnvMfSjWavbJ_rS-5-6V4IqDngy3YIS0m1UVyCHKEZFOFarYdtiILdkOSPnouXcn8e2a9S8V-FLN1x0RKDmTCIEhnNJa1oJVGaoJIMnTU-iIGmODp1DkpVMkJ-t98RuWhEiOd9I-H-1vfZYYtNU1tbL0MV_AYn4HyfHv1U6EQOsY1cqq8n7IYhuEvr6jy_c-QWjfXcMgbWVHXjOticaxdMyjPf1QUUdCPc_IW9UrfcwoP5CIpKVsE3W7y05QlUmODEoDICLl23oMMLlPjUWJpg3X9sxz_vgjxMubSVq-ERnfnlMNMPnEf5KGOOpT6w6mhziNfMA-T4SwPRwdB40wydHP4rUpkukseRctPsJVUh7E-FuV3W06ukpH6IqrV_3m00__y30000)
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


