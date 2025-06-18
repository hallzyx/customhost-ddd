using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

/// <summary>
/// Hotel command service implementation
/// </summary>
public class HotelCommandService(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork) 
    : IHotelCommandService
{
    /// <inheritdoc />
    public async Task<Hotel?> Handle(CreateHotelCommand command)
    {
        // Validate command
        command.Validate();
        
        // Check if hotel with same email already exists
        if (await hotelRepository.ExistsByEmailAsync(command.Email))
        {
            throw new InvalidOperationException($"Hotel with email {command.Email} already exists");
        }

        var hotel = new Hotel(command);
        try
        {
            await hotelRepository.AddAsync(hotel);
            await unitOfWork.CompleteAsync();
            return hotel;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Hotel?> Handle(UpdateHotelCommand command)
    {
        // Validate command
        command.Validate();
        
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel == null)
            return null;

        // Check if email is being changed and if new email already exists
        if (hotel.EmailAddress != command.Email.ToLowerInvariant())
        {
            if (await hotelRepository.ExistsByEmailAsync(command.Email))
            {
                throw new InvalidOperationException($"Hotel with email {command.Email} already exists");
            }
        }

        try
        {
            hotel.UpdateInfo(command.Name, command.Address, command.Email, command.Phone);
            hotelRepository.Update(hotel);
            await unitOfWork.CompleteAsync();
            return hotel;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel == null)
            return false;

        try
        {
            hotelRepository.Remove(hotel);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }
}
