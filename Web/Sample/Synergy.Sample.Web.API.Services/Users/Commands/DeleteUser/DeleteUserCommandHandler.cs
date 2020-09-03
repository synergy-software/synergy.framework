using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Commands;
using Synergy.Sample.Web.API.Services.Infrastructure.Exceptions;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Commands.DeleteUser
{
    [CreatedImplicitly]
    public class DeleteUserCommandHandler : IDeleteUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public DeleteUserCommandResult Handle(DeleteUserCommand command)
        {
            var user = this._userRepository.FindUserBy(command.UserId);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id {command.UserId} does not exist");
            }

            this._userRepository.DeleteUser(command.UserId);

            return new DeleteUserCommandResult();
        }
    }

    public interface IDeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserCommandResult>{}
}