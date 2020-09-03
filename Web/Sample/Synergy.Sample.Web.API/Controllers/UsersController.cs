using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synergy.Sample.Web.API.Services.Infrastructure.Commands;
using Synergy.Sample.Web.API.Services.Infrastructure.Queries;
using Synergy.Sample.Web.API.Services.Users.Commands.CreateUser;
using Synergy.Sample.Web.API.Services.Users.Commands.DeleteUser;
using Synergy.Sample.Web.API.Services.Users.Queries.GetUser;
using Synergy.Sample.Web.API.Services.Users.Queries.GetUsers;

namespace Synergy.Sample.Web.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this._queryDispatcher = queryDispatcher;
            this._commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// Returns all users stored in the system.
        /// </summary>
        [HttpGet]
        public GetUsersQueryResult GetUsers()
        {
            return this._queryDispatcher.Dispatch<GetUsersQuery, IGetUsersQueryHandler, GetUsersQueryResult>(new GetUsersQuery());
        }

        /// <summary>
        /// Gets user by provided identifier.
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>User details</returns>
        [HttpGet("{userId}", Name = nameof(UsersController.GetUser))]
        public GetUserQueryResult GetUser(string userId)
        {
            return this._queryDispatcher.Dispatch<GetUserQuery, IGetUserQueryHandler, GetUserQueryResult>(new GetUserQuery(userId));
        }

        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/users
        ///     {
        ///        "login": "marcin@synergy.com",
        ///     }
        /// 
        /// </remarks>
        /// <param name="version">API version</param>
        /// <param name="user">Details of user to create</param>
        /// <returns>A newly created User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the request contains invalid data</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
        public ActionResult<CreateUserCommandResult> Create([FromRoute] string version, [FromBody] CreateUserCommand user)
        {
            var created = this._commandDispatcher.Dispatch<CreateUserCommand, ICreateUserCommandHandler, CreateUserCommandResult>(user);
            return this.CreatedAtRoute(nameof(this.GetUser), new {version, userId = created.User.Id}, created);
        }

        /// <summary>
        /// Deletes user with specified identifier
        /// </summary>
        /// <param name="userId">Identifier of the user to delete</param>
        /// <returns>Result of the operation</returns>
        [HttpDelete("{userId}")]
        public DeleteUserCommandResult DeleteUser(string userId)
        {
            return this._commandDispatcher.Dispatch<DeleteUserCommand, IDeleteUserCommandHandler, DeleteUserCommandResult>(new DeleteUserCommand(userId));
        }
    }
}