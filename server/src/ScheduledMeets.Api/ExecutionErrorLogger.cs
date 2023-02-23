using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;

using Microsoft.Extensions.Logging;

using System.Text;

namespace ScheduledMeets.Api
{
    public class ExecutionErrorLogger : ExecutionDiagnosticEventListener
    {
        private readonly ILogger _logger;

        public ExecutionErrorLogger(ILogger<ExecutionErrorLogger> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void RequestError(IRequestContext context, Exception exception)
        {
            _logger.LogError(exception, "Request error");
        }

        public override void ValidationErrors(IRequestContext context,
            IReadOnlyList<IError> errors)
        {
            if (!_logger.IsEnabled(LogLevel.Debug))
                return;

            StringBuilder messageBuilder = new("Validation errors\n");

            for (int index = 0; index < errors.Count; index++)
            {
                IError error = errors[index];
                messageBuilder
                    .AppendFormat(
                        "[{0}]\n" +
                        "\tMessage: {1}\n" +
                        "\tPath: {2}\n",
                        index + 1,
                        error.Message,
                        error.Path);
            }

            _logger.LogDebug(messageBuilder.ToString());
        }

        public override void TaskError(IExecutionTask task, IError error)
        {
            _logger.LogError("Resolver error\n" +
                "Message: {Message}\n" +
                "Path: {Path}\n" +
                "Code: {Code}\n" +
                "Exception: {Exception}\n",
                error.Message,
                error.Path,
                error.Code,
                error.Exception);
        }

        public override void ResolverError(IMiddlewareContext context, IError error)
        {
            _logger.LogError("Resolver error\n" +
                "Message: {Message}\n" +
                "Path: {Path}\n" +
                "Exception: {Exception}\n",
                error.Message,
                error.Path,
                error.Exception);
        }

        public override void SyntaxError(IRequestContext context, IError error)
        {
            _logger.LogError("Syntax error\n" +
                "Message: {Message}\n" +
                "Path: {Path}\n" +
                "Code: {Code}\n" +
                "Exception: {Exception}\n",
                error.Message,
                error.Path,
                error.Exception,
                error.Code);
        }

        public override void SubscriptionEventError(SubscriptionEventContext context,
            Exception exception)
        {
            _logger.LogError(exception, "Subscription event error");
        }

        public override void SubscriptionTransportError(ISubscription subscription,
            Exception exception)
        {
            _logger.LogError(exception, "Subscription transport error");
        }
    }
}
