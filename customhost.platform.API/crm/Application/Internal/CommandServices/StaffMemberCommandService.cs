using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

public class StaffMemberCommandService(IStaffMemberRepository staffMemberRepository, IUnitOfWork unitOfWork) 
    : IStaffMemberCommandService
{
    public async Task<StaffMember?> Handle(CreateStaffMemberCommand command)
    {
        // Check if email already exists
        if (await staffMemberRepository.ExistsByEmailAsync(command.Email))
        {
            throw new InvalidOperationException($"Staff member with email {command.Email} already exists");
        }

        var staffMember = new StaffMember(command);
        
        try
        {
            await staffMemberRepository.AddAsync(staffMember);
            await unitOfWork.CompleteAsync();
            return staffMember;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<StaffMember?> Handle(UpdateStaffMemberCommand command)
    {
        var staffMember = await staffMemberRepository.FindByIdAsync(command.Id);
        if (staffMember == null) return null;

        // Check if email is being changed and if new email already exists
        if (staffMember.Email != command.Email.ToLowerInvariant() &&
            await staffMemberRepository.ExistsByEmailAndIdNotAsync(command.Email, command.Id))
        {
            throw new InvalidOperationException($"Staff member with email {command.Email} already exists");
        }

        staffMember.UpdateInfo(command.FirstName, command.LastName, command.Email, command.Phone, command.Department);
        
        try
        {
            staffMemberRepository.Update(staffMember);
            await unitOfWork.CompleteAsync();
            return staffMember;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> Handle(DeleteStaffMemberCommand command)
    {
        var staffMember = await staffMemberRepository.FindByIdAsync(command.Id);
        if (staffMember == null) return false;
        
        try
        {
            staffMemberRepository.Remove(staffMember);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<StaffMember?> Handle(ChangeStaffMemberStatusCommand command)
    {
        var staffMember = await staffMemberRepository.FindByIdAsync(command.Id);
        if (staffMember == null) return null;

        switch (command.Status)
        {
            case customhost_backend.crm.Domain.Models.ValueObjects.StaffStatus.Active:
                staffMember.Activate();
                break;
            case customhost_backend.crm.Domain.Models.ValueObjects.StaffStatus.Inactive:
                staffMember.Deactivate();
                break;
            case customhost_backend.crm.Domain.Models.ValueObjects.StaffStatus.OnLeave:
                staffMember.SetOnLeave();
                break;
            case customhost_backend.crm.Domain.Models.ValueObjects.StaffStatus.Terminated:
                staffMember.Terminate();
                break;
        }
        
        try
        {
            staffMemberRepository.Update(staffMember);
            await unitOfWork.CompleteAsync();
            return staffMember;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
