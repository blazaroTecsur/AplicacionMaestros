using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application.Behaviors;

public sealed class ExceptionHandlingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehavior(
        ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            var errors = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning(
                "Validación fallida para {Request}: {Errors}",
                typeof(TRequest).Name,
                errors);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error no controlado al procesar {Request}",
                typeof(TRequest).Name);
            throw;
        }
    }
}
