using System;
using Newtonsoft.Json.Linq;
using Synergy.Sample.Web.API.Services.Users.Commands.CreateUser;
using Synergy.Sample.Web.API.Services.Users.Domain;
using Synergy.Sample.Web.API.Tests.Infrastructure;
using Synergy.Web.Api.Testing;

namespace Synergy.Sample.Web.API.Tests.Users
{
    public class UsersClient
    {
        private const string Path = "api/v1/users";
        private readonly TestServer _testServer;

        public UsersClient(TestServer testServer)
        {
            this._testServer = testServer;
        }

        public HttpOperation GetAll()
            => this._testServer.Get(UsersClient.Path)
                   .Details("Get list of users");
        
        public HttpOperation GetUserBy(Uri location)
            => this._testServer.Get(location.ToString())
                   .Details($"Get created user");

        public HttpOperation GetUser(string userId)
            => this._testServer.Get($"{UsersClient.Path}/{userId}")
                   .Details($"Get user by id");
        
        public CreateUserOperation Create(string login)
            => this._testServer.Post<CreateUserOperation>(UsersClient.Path, body: JObject.Parse($"{{login:{login.QuoteOrNull()}}}"))
                   .Details($"Create a new user with login {login.QuoteOrNull()}");

        public CreateUserOperation Create(Login login)
            => this._testServer.Post<CreateUserOperation>(UsersClient.Path, body: new CreateUserCommand{Login = login})
                   .Details($"Create a new user with login {login}");

        public HttpOperation DeleteUser(string userId)
            => this._testServer.Delete($"{UsersClient.Path}/{userId}")
                   .Details($"Delete user by id");

        public class CreateUserOperation : HttpOperation
        {
            public CreateUserOperation ReadUserId(out string userId)
            {
                this.Response.Content.Read("user.id", out userId);
                return this;
            }

            public CreateUserOperation ReadCreatedUserLocationUrl(out Uri location)
            {
                location = this.Response.Headers.Location;
                return this;
            }
        }
    }
}