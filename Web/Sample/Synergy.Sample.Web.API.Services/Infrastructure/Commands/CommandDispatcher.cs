using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Synergy.Contracts;

namespace Synergy.Sample.Web.API.Services.Infrastructure.Commands
{
    [UsedImplicitly]
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandHandlerFactory commandHandlerFactory;
        private readonly ILogger<CommandDispatcher> _logger;

        public CommandDispatcher(ICommandHandlerFactory commandHandlerFactory, ILogger<CommandDispatcher> logger)
        {
            this.commandHandlerFactory = commandHandlerFactory;
            this._logger = logger;
        }

        public TCommandResult Dispatch<TCommand, TCommandHandler, TCommandResult>(TCommand command)
            where TCommand : class
            where TCommandResult : class
            where TCommandHandler : ICommandHandler<TCommand, TCommandResult>
        {
            Fail.IfArgumentNull(command);
            var commandHandler = this.commandHandlerFactory.Create<TCommand, TCommandResult>(command);
            this._logger.LogTrace("Command {Command} dispatch started by {CommandHandler}", command.GetType().Name, commandHandler.GetType().Name);

            try
            {
                var result = commandHandler.Handle(command);
                return result;
            }
            finally
            {
                this.commandHandlerFactory.Destroy(commandHandler);
            }
        }
    }

    public interface ICommandDispatcher
    {
        TCommandResult Dispatch<TCommand, TCommandHandler, TCommandResult>([NotNull] TCommand command)
            where TCommand : class
            where TCommandResult : class
            where TCommandHandler : ICommandHandler<TCommand, TCommandResult>;
    }
}