using System.ComponentModel.DataAnnotations;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Commands.CreateUser
{
    public sealed class CreateUserCommand
    {
        [Required]
        public Login Login { get; set; }
    }
}