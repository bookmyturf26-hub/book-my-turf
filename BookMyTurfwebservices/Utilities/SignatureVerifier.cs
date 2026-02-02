using System.Security.Cryptography;
using System.Text;

namespace BookMyTurfwebservices.Utilities;

public static class SignatureVerifier
{
    public static bool VerifyRazorpaySignature(
        string payload,
        string signature,
        string secret)
    {
        try
        {
            var generatedSignature = GenerateHMACSHA256Signature(payload, secret);
            return signature == generatedSignature;
        }
        catch
        {
            return false;
        }
    }

    public static string GenerateHMACSHA256Signature(string payload, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public static bool VerifyPaymentSignature(
        Dictionary<string, string> attributes,
        string secret)
    {
        var signature = attributes["razorpay_signature"];
        attributes.Remove("razorpay_signature");

        var message = string.Join("|",
            attributes["razorpay_order_id"],
            attributes["razorpay_payment_id"]);

        var generatedSignature = GenerateHMACSHA256Signature(message, secret);
        return signature == generatedSignature;
    }
}