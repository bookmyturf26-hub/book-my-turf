using BookMyTurfwebservices.Models.DTOs.Requests;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Services.Interfaces;
using BookMyTurfwebservices.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookMyTurfwebservices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;
    private readonly IIdempotencyChecker _idempotencyChecker;

    public PaymentController(
        IPaymentService paymentService,
        ILogger<PaymentController> logger,
        IIdempotencyChecker idempotencyChecker)
    {
        _paymentService = paymentService;
        _logger = logger;
        _idempotencyChecker = idempotencyChecker;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            var order = await _paymentService.CreateOrderAsync(request);
            return Ok(new ApiResponse<OrderResponse>
            {
                Success = true,
                Data = order
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Failed to create payment order"
            });
        }
    }

    [HttpPost("verify-payment")]
    public async Task<IActionResult> VerifyPayment([FromBody] VerifyPaymentRequest request)
    {
        try
        {
            var verification = await _paymentService.VerifyPaymentAsync(request);
            return Ok(new ApiResponse<PaymentVerificationResponse>
            {
                Success = true,
                Data = verification
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying payment");
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Payment verification failed"
            });
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> PaymentWebhook()
    {
        try
        {
            // Read raw request body for signature verification
            using var reader = new StreamReader(Request.Body);
            var requestBody = await reader.ReadToEndAsync();

            // Parse the webhook event
            var webhookEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookEvent>(requestBody);

            if (webhookEvent == null)
            {
                return BadRequest("Invalid webhook payload");
            }

            // Handle the webhook
            await _paymentService.HandlePaymentWebhookAsync(webhookEvent);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Webhook processing failed");
            return StatusCode(500);
        }
    }
}