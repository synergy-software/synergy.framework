using JetBrains.Annotations;

namespace Synergy.Sample.Web.API.Services.Infrastructure.Queries
{
    public interface IQueryHandler<in TQuery, out TQueryResult>
    {
        TQueryResult Handle([NotNull] TQuery query);
    }
}