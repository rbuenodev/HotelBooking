using Application.Payment.Enums;

namespace Application.Payment.DTOs
{
    public class PaymentStateDTO
    {
        public Status Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }
}
