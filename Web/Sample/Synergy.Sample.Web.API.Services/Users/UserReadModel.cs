using Newtonsoft.Json;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users
{
    public class UserReadModel
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("login")]
        public Login Login { get; }

        public UserReadModel(string id, Login login)
        {
            this.Id = id;
            this.Login = login;
        }

        public UserReadModel(User user)
        {
            this.Id = user.Id;
            this.Login = user.Login;
        }
    }
}