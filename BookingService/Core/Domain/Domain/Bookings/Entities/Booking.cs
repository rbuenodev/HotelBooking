using Domain.Bookings.Enums;
using Domain.Bookings.Exceptions;
using Domain.Bookings.Ports;
using Action = Domain.Bookings.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Status Status { get; set; }        
        public Room Room { get; set; }
        public Guest Guest { get; set; }

        public Booking()
        {
            Status = Status.Created;
        }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finishied,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        private void ValidateState()
        {
            if (this.PlacedAt == default(DateTime))
            {
                throw new RequiredDateException("PlacedAt date is null");
            }
            if (this.Start == default(DateTime))
            {
                throw new RequiredDateException("Start date is null");
            }
            if (this.End == default(DateTime))
            {
                throw new RequiredDateException("End date is null");
            }
            if (this.Room == null)
            {
                throw new RequiredRoomIsNullExeption();
            }
            if (this.Guest == null)
            {

                throw new RequiredGuestIsNullException();
            }     
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();
            this.Guest.IsValid();
            this.Room.CanBeBooked();

            if (this.Id == 0)
            {
                var resp = await bookingRepository.CreateBooking(this);
                this.Id = resp.Id;
            }
        }
    }
}
