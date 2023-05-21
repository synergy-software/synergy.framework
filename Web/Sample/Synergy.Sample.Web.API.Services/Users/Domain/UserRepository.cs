using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Contracts;
using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;

namespace Synergy.Sample.Web.API.Services.Users.Domain
{
    [CreatedImplicitly]
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new();

        [SequenceDiagramDatabaseCall("INSERT INTO Users (Id, Login) VALUES (@Id, @Login)")]
        public User CreateUser(Login login)
        {
            var user = new User(Guid.NewGuid().ToString().Replace("-", ""), login);
            this.users.Add(user);
            return user;
        }

        public User? FindUserBy(string userId)
        {
            var user = this.users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        private User GetUserBy(string userId)
        {
            var user = this.FindUserBy(userId);
            Fail.IfNull(user, Violation.Of("There is no user with id '{0}'", userId));
            return user!;
        }

        public void DeleteUser(string userId)
        {
            var user = this.GetUserBy(userId);
            this.users.Remove(user);
        }

        public ReadOnlyCollection<User> GetAllUsers()
        {
            return this.users.AsReadOnly();
        }
    }

    public interface IUserRepository
    {
        User CreateUser(Login login);
        User? FindUserBy(string userId);
        void DeleteUser(string userId);
        ReadOnlyCollection<User> GetAllUsers();
    }
}