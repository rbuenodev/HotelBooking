using Application.Guest.DTOs;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Guests.Ports;
using Moq;
using Application.Enum;
using Domain.Guests.ValueObjects;
using Domain.Guests.Enums;
using Application.Guest;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        private GuestManager guestManager;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ShouldCreateAGuest()
        {
            var expectedId = 222;

            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var guestDto = new GuestDTO { Name = "Rich", LastName = "Bueno", Email = "rich@email.com", IdNumber = "1234", IdTypeCode = 1, Id = expectedId };
            var request = new CreateGuestRequest() { Data = guestDto };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.That(expectedId, Is.EqualTo(res.Data.Id));
            Assert.That(guestDto.Name, Is.EqualTo(res.Data.Name));
        }

        [TestCase("a")]
        [TestCase("b")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("abc")]
        public async Task ShouldReturnInvalidPersonDocumentIdExceptionWhenDocsAreInvalid(string docNumber)
        {
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(111));
            guestManager = new GuestManager(fakeRepo.Object);

            var guestDto = new GuestDTO { Name = "Rich", LastName = "Bueno", Email = "rich@email.com", IdNumber = docNumber, IdTypeCode = 1 };
            var request = new CreateGuestRequest() { Data = guestDto };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.INVALID_PERSON_ID));
            Assert.That(res.Message, Is.EqualTo("The given ID is not valid"));
        }

        [TestCase("", "lastNameTest", "email@email.com")]
        [TestCase("firstName", "", "email@email.com")]
        [TestCase("firsName", "lastNameTest", "")]
        [TestCase(null, "", "")]

        public async Task ShouldReturnMissingRequiredInformationExceptionWhenInformationsAreInvalid(string name, string lastName, string email)
        {
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(111));
            guestManager = new GuestManager(fakeRepo.Object);

            var guestDto = new GuestDTO { Name = name, LastName = lastName, Email = email, IdNumber = "12345", IdTypeCode = 1 };
            var request = new CreateGuestRequest() { Data = guestDto };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.MISSING_REQUIRED_INFORMATION));
            Assert.That(res.Message, Is.EqualTo("Missing required information"));
        }

        public async Task ShouldReturnGuestNotFound()
        {
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Get(222)).Returns(Task.FromResult<Guest?>(null));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.GetGest(122);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.GUEST_NOT_FOUND));
            Assert.That(res.Message, Is.EqualTo("Guest record was not found with the given Id"));
        }

        [Test]
        public async Task ShouldGetAGuest()
        {
            var expectedId = 222;
            var guest = new Guest { Name = "Name", LastName = "LastName", DocumentId = new PersonId { DocumentType = DocumentType.Passport, IdNumber = "1234" }, Email = "email@email.com", Id = expectedId };            
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Get(expectedId)).Returns(Task.FromResult<Guest?>(guest));

            guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.GetGest(expectedId);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.That(expectedId, Is.EqualTo(res.Data.Id));
        }
    }
}