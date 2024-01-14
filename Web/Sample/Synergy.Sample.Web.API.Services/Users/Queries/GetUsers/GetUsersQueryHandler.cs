using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Queries;
using Synergy.Sample.Web.API.Services.Users.Domain;

namespace Synergy.Sample.Web.API.Services.Users.Queries.GetUsers
{
    [CreatedImplicitly]
    public class GetUsersQueryHandler : IGetUsersQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [SequenceDiagramCall(typeof(IUserRepository), nameof(IUserRepository.GetAllUsers))]
        [SequenceDiagramActivation(typeof(GetUsersQueryResult))]
        public GetUsersQueryResult Handle(GetUsersQuery query)
        {
            var users = this._userRepository.GetAllUsers();
            return new GetUsersQueryResult(users);
        }
    }

    public interface IGetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryResult> { }
}