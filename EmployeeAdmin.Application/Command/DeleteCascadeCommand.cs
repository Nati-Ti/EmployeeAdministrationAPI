using MediatR;

namespace EmployeeAdmin.Application.Command
{
    public record DeleteCascadeCommand(Guid Id) : IRequest<String>;

}
