using Domain.Rooms.Enums;

namespace Domain.Rooms.ValueObjects
{
    public class Price
    {
        public decimal Value { get; set; }  
        public AcceptedCurrecies Currency { get; set; }
    }
}
