using MediatR;

namespace EmployeeAdmin.Application.Command
{
    public record DeleteAssignCommand(Guid id) : IRequest<String>;

}
