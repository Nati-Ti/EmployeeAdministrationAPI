using EmployeeAdmin.Application.DTO;
using MediatR;

namespace EmployeeAdmin.Application.Command
{
    public record CreatePositionCommand(EmployeePositionInputDto InputDto) : IRequest<EmployeePositionInputDto>;

}
