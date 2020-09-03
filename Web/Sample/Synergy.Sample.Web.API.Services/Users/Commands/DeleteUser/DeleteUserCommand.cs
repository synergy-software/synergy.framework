namespace Synergy.Sample.Web.API.Services.Users.Commands.DeleteUser {
    public sealed class DeleteUserCommand
    {
        public string UserId { get; }

        public DeleteUserCommand(string userId)
        {
            this.UserId = userId;
        }
    }
}