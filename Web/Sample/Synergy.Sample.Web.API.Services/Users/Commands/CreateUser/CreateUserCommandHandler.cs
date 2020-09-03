using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Commands;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Commands.CreateUser
{
    [CreatedImplicitly]
    public class CreateUserCommandHandler : ICreateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public CreateUserCommandResult Handle(CreateUserCommand command)
        {
            // TODO: Add validation (bad-request) mechanism - maybe use data annotations?
            var user = this._userRepository.CreateUser(command.Login);
            return new CreateUserCommandResult(user);
        }
    }

    public interface ICreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResult> { }
}