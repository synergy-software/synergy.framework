using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Synergy.Sample.Web.API.Services.Infrastructure;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Queries.GetUsers
{
    public sealed class GetUsersQueryResult
    {
        [JsonProperty("users")]
        public ReadOnlyCollection<UserReadModel> Users { get; }

        public GetUsersQueryResult(ReadOnlyCollection<User> users)
        {
            this.Users = users.ConvertAll(user => new UserReadModel(user));
        }
    }
}