using Application.Payment.DTOs;
using Application.Payment.Responses;

namespace Application.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResponse> PayWithCreditCard(string paymentIntention);
        Task<PaymentResponse> PayWithDebitCard(string paymentIntention);
        Task<PaymentResponse> PayWithBankTransferCard(string paymentIntention);
    }
}
