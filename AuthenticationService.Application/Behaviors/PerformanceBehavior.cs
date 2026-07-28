using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AuthenticationService.Application.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

    public PerformanceBehavior(
        ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        var stopwatch = Stopwatch.StartNew();

        var response = await next();

        stopwatch.Stop();

        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            _logger.LogWarning(
                "Long Running Request: {RequestName} ({ElapsedMilliseconds} ms)",
                requestName,
                elapsedMilliseconds);
        }
        else
        {
            _logger.LogInformation(
                "Request {RequestName} completed in {ElapsedMilliseconds} ms",
                requestName,
                elapsedMilliseconds);
        }

        return response;
    }
}