namespace Synergy.Sample.Web.API.Services.Users.Queries.GetUser
{
    public sealed class GetUserQuery
    {
        public string UserId { get; }

        public GetUserQuery(string userId)
        {
            this.UserId = userId;
        }
    }
}