using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Contracts;
using Synergy.Documentation.Code;
using Synergy.Sample.Web.API.Services.Infrastructure.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Exceptions;
using Synergy.Sample.Web.API.Services.Infrastructure.Queries;
using Synergy.Sample.Web.API.Services.Users.Domain;
using Synergy.Sample.Web.API.Services.Users.Queries.GetUsers;

namespace Synergy.Sample.Web.API.Services.Users.Queries.GetUser
{
    [CreatedImplicitly]
    public class GetUserQueryHandler : IGetUserQueryHandler
    {
        public static CodeFile CodeFile => CodeFile.Current();

        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [SequenceDiagramCall(typeof(IUserRepository), nameof(IUserRepository.FindUserBy))]
        [SequenceDiagramActivation(typeof(ResourceNotFoundException), Group = SequenceDiagramGroupType.Alt, GroupHeader = "if user not found")]
        [SequenceDiagramActivation(typeof(GetUsersQueryResult))]
        public GetUserQueryResult Handle(GetUserQuery query)
        {
            var user = this._userRepository.FindUserBy(query.UserId);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id {query.UserId} does not exist");
            }

            return new GetUserQueryResult(user);
        }
    }

    public interface IGetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>{}
}