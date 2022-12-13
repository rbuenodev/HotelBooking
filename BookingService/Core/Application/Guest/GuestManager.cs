using Application.Enum;
using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Guests.Exceptions;
using Domain.Guests.Ports;

namespace Application.Guest
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);
                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (InvalidPersonDocumentException)
            {
                return new GuestResponse
                {                    
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                    Message = "The given ID is not valid"
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new GuestResponse
                {                 
                    Success = false,
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {                 
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_EMAIL,
                    Message = "The given Email is not valid"
                };
            }
            catch (Exception e)
            {
                return new GuestResponse
                {                    
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = $"There was an error while saving {e.Message}"
                };
            }
        }

        public async Task<GuestResponse> GetGest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);
            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                    Message = "Guest record was not found with the given Id"
                };
            }
            return new GuestResponse { Success = true, Data = GuestDTO.MapToDto(guest) };
        }
    }
}
