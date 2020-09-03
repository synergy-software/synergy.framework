using Newtonsoft.Json;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Queries.GetUser
{
    public sealed class GetUserQueryResult
    {
        [JsonProperty("user")]
        public UserReadModel User { get; }

        public GetUserQueryResult(User user)
        {
            this.User = new UserReadModel(user);
        }
    }
}