using Polly;
using Polly.Retry;

namespace AplicacionMaestro.Worker.Almacenes.Resilience
{
    public static class RetryPolicies
    {
        public static AsyncRetryPolicy CreateDefaultRetryPolicy(
            ILogger logger)
        {
            return Policy
                .Handle<Exception>(ex => ex is not OperationCanceledException)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2s, 4s, 8s
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        logger.LogWarning(
                            exception,
                            "Retry {Retry} en {Delay}s",
                            retryCount,
                            timeSpan.TotalSeconds);
                    }
                );
        }
    }
}
