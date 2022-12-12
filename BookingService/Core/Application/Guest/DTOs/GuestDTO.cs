using Domain.Guests.Enums;
using Domain.Guests.ValueObjects;
using Entities = Domain.Entities;

namespace Application.Guest.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Guest MapToEntity(GuestDTO guestDTO)
        {
            return new Entities.Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                LastName = guestDTO.LastName,
                Email = guestDTO.Email,
                DocumentId = new PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode,
                }
            };
        }

        public static GuestDTO MapToDto(Entities.Guest guest)
        {
            return new GuestDTO
            {
                Id = guest.Id,
                Name = guest.Name,
                LastName = guest.LastName,
                Email = guest.Email,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType,
            };
        }
    }
}
