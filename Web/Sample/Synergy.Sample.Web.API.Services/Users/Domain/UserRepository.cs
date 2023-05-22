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

        [SequenceDiagramActivation(typeof(UserRepository))]
        [SequenceDiagramSelfCall(nameof(UserRepository.InstantiateUserEntity))]
        [SequenceDiagramDatabaseCall("INSERT INTO Users (Id, Login) VALUES (@Id, @Login)")]
        [SequenceDiagramDeactivation(typeof(UserRepository))]
        public User CreateUser(Login login)
        {
            var user = UserRepository.InstantiateUserEntity(login);
            this.users.Add(user);
            return user;
        }

        [SequenceDiagramActivation(typeof(UserRepository))]
        [SequenceDiagramActivation(typeof(User))]
        [SequenceDiagramDeactivation(typeof(UserRepository))]
        private static User InstantiateUserEntity(Login login)
        {
            return new User(
                Guid.NewGuid()
                    .ToString()
                    .Replace("-", ""),
                login
            );
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