using Domain.Entities;

namespace Domain.Rooms.Ports
{
    public interface IRoomRepository
    {
        Task<Room?> Get(int id);
        Task<int> Create(Room room);
        Task<Room> GetAggregate(int id);        
    }
}
