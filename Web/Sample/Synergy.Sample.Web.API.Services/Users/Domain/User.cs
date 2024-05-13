using System;

namespace Synergy.Sample.Web.API.Services.Users.Domain
{
    public class User
    {
        public string Id { get; }
        public Login Login { get; }
        public DateTime CreatedOn { get; }

        public User(string id, Login login)
        {
            this.Id = id;
            this.Login = login;
            this.CreatedOn = DateTime.Now;
        }
    }
}
