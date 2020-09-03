using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Synergy.Contracts;

namespace Synergy.Sample.Web.API.Services.Infrastructure.Queries
{
    [UsedImplicitly]
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IQueryHandlerFactory queryHandlerFactory;
        private readonly ILogger<QueryDispatcher> _logger;

        public QueryDispatcher(IQueryHandlerFactory queryHandlerFactory, ILogger<QueryDispatcher> logger)
        {
            this.queryHandlerFactory = queryHandlerFactory;
            this._logger = logger;
        }

        public TQueryResult Dispatch<TQuery, TQueryHandler, TQueryResult>(TQuery query)
            where TQuery : class
            where TQueryResult : class
            where TQueryHandler : IQueryHandler<TQuery, TQueryResult>
        {
            Fail.IfArgumentNull(query, nameof(query));
            var queryHandler = this.queryHandlerFactory.Create<TQuery, TQueryResult>(query);
            this._logger.LogTrace("Query {Query} dispatch started by {QueryHandler}", query.GetType().Name, queryHandler.GetType().Name);

            try
            {
                var result = queryHandler.Handle(query);
                return result;
            }
            finally
            {
                this.queryHandlerFactory.Destroy(queryHandler);
            }
        }
    }

    public interface IQueryDispatcher
    {
        TQueryResult Dispatch<TQuery, TQueryHandler, TQueryResult>([NotNull] TQuery query)
            where TQuery : class
            where TQueryResult : class
            where TQueryHandler : IQueryHandler<TQuery, TQueryResult>;
    }
}