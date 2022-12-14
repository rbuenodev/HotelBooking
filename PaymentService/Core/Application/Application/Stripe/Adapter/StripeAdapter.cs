using Application.Enum;
using Application.Payment.DTOs;
using Application.Payment.Enums;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Payment.Application.Exceptions;

namespace Payment.Application.Stripe.Adapter
{
    public class StripeAdapter : IPaymentProcessor
    {
        public Task<PaymentResponse> CapturePayment(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentIntention))
                {
                    throw new InvalidPaymentIntentionException();
                }

                paymentIntention += "/success";

                Random random = new Random();
                var dto = new PaymentStateDTO
                {
                    CreatedDate = DateTime.UtcNow,
                    Message = $"Successfully paid {paymentIntention}",
                    PaymentId = random.Next(100).ToString(),
                    Status = Status.Success,
                };

                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Payment successfully processed"
                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntentionException)
            {
                var resp = new PaymentResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                    Message = "The selected payment intention is invalid"
                };
                return Task.FromResult(resp);
            }
        }
    }
}
