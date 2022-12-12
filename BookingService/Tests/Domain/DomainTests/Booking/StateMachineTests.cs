using Domain.Bookings.Enums;
using Domain.Entities;

using Action = Domain.Bookings.Enums.Action;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForBookWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Paid));
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Canceled));
        }

        [Test]
        public void ShouldSetStatusToFinishWhenFinishingAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Finishied));
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundingAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Refounded));
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningACanceledBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Refound);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundABookingWithFinishedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Finishied));
        }
    }
}