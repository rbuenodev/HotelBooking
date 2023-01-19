using Application.Booking.Enums;
using Application.Payment.Ports;
using Payment.Application.Stripe.Adapter;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Application
{

    [ExcludeFromCodeCoverage]
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProvider)
        {
            switch (selectedPaymentProvider)
            {
                case SupportedPaymentProviders.Stripe:
                    return new StripeAdapter();

                default: return new NotImplementedPaymentProvider();
            }
        }
    }
}
