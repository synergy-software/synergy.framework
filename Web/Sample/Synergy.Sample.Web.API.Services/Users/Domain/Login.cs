using Synergy.Contracts;

namespace Synergy.Sample.Web.API.Services.Users.Domain
{
    public struct Login
    {
        public string Value { get; }

        public Login(string value) => this.Value = value.OrFailIfWhiteSpace(nameof(Login));

        public override string ToString() => this.Value;
    }
}