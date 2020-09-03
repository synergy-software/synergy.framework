using Newtonsoft.Json;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandResult
    {
        [JsonProperty("user")]
        public UserReadModel User { get; }

        public CreateUserCommandResult(User user)
        {
            this.User = new UserReadModel(user);
        }
    }
}