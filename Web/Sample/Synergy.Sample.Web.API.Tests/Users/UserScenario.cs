using System;
using System.Net;
using JetBrains.Annotations;
using Synergy.Sample.Web.API.Services.Users.Domain;
using Synergy.Sample.Web.API.Tests.Infrastructure;
using Synergy.Web.Api.Testing;
using Synergy.Web.Api.Testing.Assertions;
using Synergy.Web.Api.Testing.Features;
using Synergy.Web.Api.Testing.Json;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Users
{
    public class UserScenario : IDisposable
    {
        private SampleTestServer testServer;
        private UsersClient users;
        private const string Path = @"../../../Users";
        private readonly Feature feature = new Feature("Manage users through API");
        private readonly Ignore errorNodes = Ignore.ResponseBody("traceId", "title");

        public UserScenario()
        {
            this.testServer = new SampleTestServer();
            this.users = new UsersClient(this.testServer);
            this.testServer.Repair = Repair.Mode;
        }

        public void Dispose()
        {
            this.testServer.Dispose();
        }

        [Fact]
        public void manage_users_through_web_api()
        {
            // SCENARIO
            this.GetEmptyListOfUsers();
            var userId = this.CreateUser();
            this.GetUser(userId);
            this.GetListOfUsers();
            this.TryToCreateUserWithErrors();
            this.DeleteUser(userId);

            // DOCUMENTATION
            new Markdown(this.feature).GenerateReportTo(UserScenario.Path + "/Users.md");
        }

        private void GetEmptyListOfUsers()
        {
            var scenario = this.feature.Scenario("Get empty list of users");

            this.users.GetAll()
                .InStep(scenario.Step("Retrieve users"))
                .ShouldBe(ApiConventionFor.GetListOfResources())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S01_E01_GetEmptyListOfUsers.json")
                        .Expected("Manual: Empty users list is returned")
                );
        }

        private string CreateUser()
        {
            var scenario = this.feature.Scenario("Create a user");

            this.users.Create(new Login("marcin@synergy.com"))
                .InStep(scenario.Step("Create new user"))
                .ShouldBe(ApiConventionFor.CreateResource())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S02_E01_CreateUser.json")
                        .Ignore(Ignore.ResponseLocationHeader())
                        .Ignore(Ignore.ResponseBody("user.id"))
                        .Expected("Manual: User is created and its details are returned"))
                .ReadUserId(out var id)
                .ReadCreatedUserLocationUrl(out var location);

            this.users.GetUserBy(location)
                .InStep(scenario.Step("Get created user pointed by \"Location\" header"))
                .ShouldBe(ApiConventionFor.GetSingleResource())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S02_E02_GetCreatedUserByLocation.json")
                        .Ignore(Ignore.RequestMethod())
                        .Ignore(Ignore.ResponseBody("user.id"))
                        .Expected("Manual: User details are returned"));

            return id;
        }

        private void GetUser(string userId)
        {
            var scenario = this.feature.Scenario("Get user");

            this.users.GetUser(userId)
                .InStep(scenario.Step("Get user by id"))
                .ShouldBe(ApiConventionFor.GetSingleResource())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S03_E01_GetUser.json")
                        .Ignore(Ignore.RequestMethod())
                        .Ignore(Ignore.ResponseBody("user.id"))
                        .Expected("Manual: User details are returned"));

            this.users.GetUser("user-id-that-do-not-exist")
                .InStep(scenario.Step("Negative test: Try to get user that do not exist"))
                .ShouldBe(ApiConventionFor.GetSingleResourceThatDoNotExist())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S03_N02_GetUserThatDoNotExist.json")
                        .Ignore(Ignore.RequestMethod())
                        .Ignore(this.errorNodes)
                        .Expected("Manual: No user details are returned and 404 error (not found) is returned instead"));
        }

        private void GetListOfUsers()
        {
            var scenario = this.feature.Scenario("Get list of users");

            this.users.GetAll()
                .InStep(scenario.Step("Retrieve users"))
                .ShouldBe(ApiConventionFor.GetListOfResources())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S04_E01_GetListOfUsers.json")
                        .Ignore(Ignore.ResponseBody("users[*].id"))
                        .Expected("Manual: Users list is returned")
                );
        }

        private void DeleteUser(string userId)
        {
            var scenario = this.feature.Scenario("Delete user");

            this.users.DeleteUser(userId)
                .InStep(scenario.Step("Delete user by id"))
                .ShouldBe(ApiConventionFor.DeleteResource())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S05_E01_DeleteUser.json")
                        .Ignore(Ignore.RequestMethod())
                        .Expected("Manual: User is deleted and no details are returned"));

            this.users.GetUser(userId)
                .InStep(scenario.Step("Try to get the deleted user"))
                .ShouldBe(ApiConventionFor.GetSingleResourceThatDoNotExist())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S05_E02_GetDeletedUser.json")
                        .Ignore(Ignore.RequestMethod())
                        .Ignore(this.errorNodes.And(Ignore.ResponseBody("message")))
                        .Expected("Manual: User is not found and error is returned"));
        }

        private void TryToCreateUserWithErrors()
        {
            var scenario = this.feature.Scenario("Try to create user without login");

            this.users.Create(null)
                .InStep(scenario.Step("Negative test: Create user with a null login"))
                .ShouldBe(ApiConventionFor.CreateResourceWithValidationError())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S02_N01_TryToCreateUserWithNullLogin.json")
                        .Ignore(this.errorNodes)
                        .Expected("Manual: User is NOT created and error is returned"));

            this.users.Create("")
                .InStep(scenario.Step("Negative test: Create user with an empty login"))
                .ShouldBe(ApiConventionFor.CreateResourceWithValidationError())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S02_N02_TryToCreateUserWithEmptyLogin.json")
                        .Ignore(this.errorNodes)
                        .Expected("Manual: User is NOT created and error is returned"));

            this.users.Create("  ")
                .InStep(scenario.Step("Negative test: Create user with a whitespace login"))
                .ShouldBe(ApiConventionFor.CreateResourceWithValidationError())
                .ShouldBe(
                    this.EqualToPattern("/Patterns/S02_N03_TryToCreateUserWithWhitespaceLogin.json")
                        .Ignore(this.errorNodes)
                        .Expected("Manual: User is NOT created and error is returned"));
        }

        private CompareOperationWithPattern EqualToPattern([PathReference] string file)
            => new CompareOperationWithPattern(UserScenario.Path + file);

        private static VerifyResponseStatus InStatus(HttpStatusCode expected)
            => new VerifyResponseStatus(expected);
    }
}