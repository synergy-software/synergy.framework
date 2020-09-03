namespace Synergy.Sample.Web.API.Services.Infrastructure.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand, TCommandResult> Create<TCommand, TCommandResult>(TCommand command) where TCommand : class where TCommandResult : class;
        void Destroy(object handler);
    }
}