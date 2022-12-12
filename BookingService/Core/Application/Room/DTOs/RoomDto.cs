using Application.Guest.DTOs;
using Domain.Rooms.Enums;
using Domain.Rooms.ValueObjects;
using Entities = Domain.Entities;

namespace Application.Room.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrecies Currency { get; set; }

        public static Entities.Room MapToEntity(RoomDto dto)
        {
            return new Entities.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintenance = dto.InMaintenance,
                Price = new Price { Currency = dto.Currency, Value = dto.Price }
            };
        }

        public static RoomDto MapToDto(Entities.Room guest)
        {
            return new RoomDto
            {
                Id = guest.Id,
                Level = guest.Level,
                Name = guest.Name,
                Currency = guest.Price.Currency,
                Price = guest.Price.Value,
                InMaintenance = guest.InMaintenance,
            };
        }
    }
}
