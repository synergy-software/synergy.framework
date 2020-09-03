namespace Synergy.Sample.Web.API.Services.Infrastructure.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TQueryResult> Create<TQuery, TQueryResult>(TQuery query) 
            where TQuery : class 
            where TQueryResult : class;

        void Destroy(object handler);
    }
}