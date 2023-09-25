using EmployeeAdmin.Application.DTO;
using MediatR;


namespace EmployeeAdmin.Application.Queries
{
    public record HierarchyQuery() : IRequest<IEnumerable<EmployeeHierarchyDto>>;

}
