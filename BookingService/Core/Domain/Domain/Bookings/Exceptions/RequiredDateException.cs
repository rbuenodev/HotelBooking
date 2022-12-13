namespace Domain.Bookings.Exceptions
{
    public class RequiredDateException : Exception
    {
        public RequiredDateException(string message) : base(message)
        {
        }
    }
}
