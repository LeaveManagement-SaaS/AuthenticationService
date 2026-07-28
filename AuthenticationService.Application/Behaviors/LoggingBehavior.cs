using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace AuthenticationService.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;


    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        var requestName = typeof(TRequest).Name;


        // Start timer
        var stopwatch = Stopwatch.StartNew();


        _logger.LogInformation(
            "----- Handling Request: {RequestName} -----",
            requestName);



        // Log request data
        var requestJson = JsonSerializer.Serialize(request);


        _logger.LogInformation(
            "Request Data: {RequestData}",
            requestJson);



        // Execute next behavior / handler
        var response = await next();



        stopwatch.Stop();



        _logger.LogInformation(
            "----- Completed Request: {RequestName} in {ElapsedMilliseconds} ms -----",
            requestName,
            stopwatch.ElapsedMilliseconds);



        return response;
    }
}