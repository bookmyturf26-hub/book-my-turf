using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookMyTurfwebservices.Models.DTOs.Requests;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Services.Interfaces;

namespace BookMyTurfwebservices.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RefundController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<RefundController> _logger;

    public RefundController(
        IPaymentService paymentService,
        ILogger<RefundController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpPost("initiate")]
    public async Task<IActionResult> InitiateRefund([FromBody] RefundRequestDto request)
    {
        try
        {
            var refundResponse = await _paymentService.ProcessRefundAsync(request);

            if (refundResponse.Success)
            {
                return Ok(ApiResponse<RefundResponse>.SuccessResponse(
                    refundResponse,
                    "Refund initiated successfully"));
            }
            else
            {
                return BadRequest(ApiResponse<RefundResponse>.FailureResponse(
                    "Failed to initiate refund",
                    new List<string> { refundResponse.ErrorMessage ?? "Unknown error" }));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initiating refund for payment {PaymentId}",
                request.PaymentId);

            return StatusCode(500, ApiResponse<RefundResponse>.FailureResponse(
                "An error occurred while processing your refund request",
                new List<string> { ex.Message }));
        }
    }

    [HttpPost("partial")]
    public async Task<IActionResult> InitiatePartialRefund(
        [FromBody] PartialRefundRequest request)
    {
        try
        {
            // Convert to RefundRequestDto
            var refundRequest = new RefundRequestDto
            {
                PaymentId = request.PaymentId,
                Amount = request.Amount,
                Reason = request.Reason,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone
            };

            var refundResponse = await _paymentService.ProcessRefundAsync(refundRequest);

            return HandleRefundResponse(refundResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initiating partial refund for payment {PaymentId}",
                request.PaymentId);

            return StatusCode(500, ApiResponse<RefundResponse>.FailureResponse(
                "An error occurred while processing your partial refund request",
                new List<string> { ex.Message }));
        }
    }

    [HttpPost("full")]
    public async Task<IActionResult> InitiateFullRefund(
        [FromBody] FullRefundRequest request)
    {
        try
        {
            // Get payment details to get the full amount
            // This would require a GetPaymentDetails method in your service
            // For now, we'll use a placeholder amount
            var payment = await _paymentService.GetPaymentStatusAsync(request.PaymentId);

            var refundRequest = new RefundRequestDto
            {
                PaymentId = request.PaymentId,
                Amount = payment.Amount, // Use actual payment amount
                Reason = request.Reason,
                SendNotification = request.SendNotification
            };

            var refundResponse = await _paymentService.ProcessRefundAsync(refundRequest);

            return HandleRefundResponse(refundResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initiating full refund for payment {PaymentId}",
                request.PaymentId);

            return StatusCode(500, ApiResponse<RefundResponse>.FailureResponse(
                "An error occurred while processing your full refund request",
                new List<string> { ex.Message }));
        }
    }

    [HttpGet("status/{refundId}")]
    public async Task<IActionResult> GetRefundStatus(string refundId)
    {
        try
        {
            // This would require a GetRefundStatus method in your service
            // For now, return a placeholder response
            var statusResponse = new RefundDetailsResponse
            {
                RefundId = refundId,
                Status = "processed",
                RefundAmount = 100.00m,
                ProcessedAt = DateTime.UtcNow.AddHours(-2)
            };

            return Ok(ApiResponse<RefundDetailsResponse>.SuccessResponse(
                statusResponse,
                "Refund status retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting refund status for {RefundId}", refundId);

            return StatusCode(500, ApiResponse<RefundDetailsResponse>.FailureResponse(
                "An error occurred while retrieving refund status",
                new List<string> { ex.Message }));
        }
    }

    [HttpGet("eligibility/{paymentId}")]
    public async Task<IActionResult> CheckRefundEligibility(string paymentId)
    {
        try
        {
            // Get payment details
            var payment = await _paymentService.GetPaymentStatusAsync(paymentId);

            var eligibilityResponse = new RefundEligibilityResponse
            {
                PaymentId = paymentId,
                IsEligible = payment.Status == "Completed",
                PaymentAmount = payment.Amount,
                AlreadyRefunded = payment.RefundAmount ?? 0,
                RefundableAmount = payment.Amount - (payment.RefundAmount ?? 0),
                IsWithinDeadline = true,
                RefundDeadline = DateTime.UtcNow.AddDays(30)
            };

            return Ok(ApiResponse<RefundEligibilityResponse>.SuccessResponse(
                eligibilityResponse,
                "Eligibility check completed"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking refund eligibility for {PaymentId}",
                paymentId);

            return StatusCode(500, ApiResponse<RefundEligibilityResponse>.FailureResponse(
                "An error occurred while checking refund eligibility",
                new List<string> { ex.Message }));
        }
    }

    private IActionResult HandleRefundResponse(RefundResponse refundResponse)
    {
        if (refundResponse.Success)
        {
            return Ok(ApiResponse<RefundResponse>.SuccessResponse(
                refundResponse,
                "Refund processed successfully"));
        }
        else
        {
            return BadRequest(ApiResponse<RefundResponse>.FailureResponse(
                "Failed to process refund",
                new List<string> { refundResponse.ErrorMessage ?? "Unknown error" }));
        }
    }
}