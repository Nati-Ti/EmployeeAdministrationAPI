using EmployeeAdmin.Application.DTO;
using MediatR;


namespace EmployeeAdmin.Application.Command
{
    public record UpdatePositionCommand(Guid id, EmployeePositionInputDto inputDto) : IRequest<EmployeePositionInputDto>;

}
