using BookMyTurfwebservices.Services;
using BookMyTurfwebservices.Services.Interfaces;
using BookMyTurfwebservices.Utilities;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register services
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRazorPayService, RazorPayService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IWebhookService, WebhookService>();
        services.AddScoped<IIdempotencyChecker, IdempotencyChecker>();

        // Add memory cache for idempotency checking
        services.AddMemoryCache();

        // Add HttpClient with Polly for resilience (SIMPLIFIED - no delegate issues)
        services.AddHttpClient("PaymentGateway")
            .AddPolicyHandler(GetRetryPolicy());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BookMyTurf Payment API",
                Version = "v1"
            });

            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "API Key Authentication",
                Name = "X-API-Key",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKey"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}