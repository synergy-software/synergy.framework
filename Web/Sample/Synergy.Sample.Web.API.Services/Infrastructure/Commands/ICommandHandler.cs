using JetBrains.Annotations;

namespace Synergy.Sample.Web.API.Services.Infrastructure.Commands
{
    public interface ICommandHandler<in TCommand, out TCommandResult>
    {
        TCommandResult Handle([NotNull] TCommand command);
    }
}