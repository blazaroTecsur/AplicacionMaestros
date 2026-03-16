using Polly;
using Polly.Retry;

namespace AplicacionMaestro.Worker.Resilience;

public static class RetryPolicies
{
    public static AsyncRetryPolicy CreateDefaultRetryPolicy(
        ILogger logger)
    {
        return Policy
            .Handle<Exception>()
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
                });
    }
}
