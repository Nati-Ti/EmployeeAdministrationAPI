using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Repositories;
using MediatR;
using EmployeeAdmin.Application.DTO;


namespace EmployeeAdmin.Application.Handler
{
    public class HierarchyHandler : IRequestHandler<HierarchyQuery, IEnumerable<EmployeeHierarchyDto>>
    {
        private readonly EmployeePositionRepository _repository;

        public HierarchyHandler(EmployeePositionRepository repository)
        {        
            _repository = repository;
        }


        public async Task<IEnumerable<EmployeeHierarchyDto>> Handle(HierarchyQuery Command, CancellationToken cancellationToken)
        {
            var allPositions = await _repository.GetAllEmployeePositionsAsync();

            var rootPositions = allPositions.Where(p => p.ParentId == null);
            //var rootPosition = await _repository.GetRootPositionAsync();

            var positionTree = new List<EmployeeHierarchyDto>();
            foreach (var root in rootPositions)
            {
                var employeeHierarchyDto = BuildPositionTree(root, allPositions);
                positionTree.Add(employeeHierarchyDto);
            }
            return positionTree;
        }

        private EmployeeHierarchyDto BuildPositionTree(Employee parentPosition, IEnumerable<Employee> allPositions)
        {
            var childPositions = allPositions.Where(p => p.ParentId == parentPosition.Id).ToList();

            var employeeHierarchyDto = new EmployeeHierarchyDto
            {
                Id = parentPosition.Id,
                Name = parentPosition.Name,
                Description = parentPosition.Description,
                ParentId = parentPosition.ParentId,
                ChildPositions = new List<EmployeeHierarchyDto>()
            };
            if (childPositions.Any())
            {
                foreach (var childPosition in childPositions)
                {
                    var childEmployeeHierarchyDto = BuildPositionTree(childPosition, allPositions);
                    employeeHierarchyDto.ChildPositions.Add(childEmployeeHierarchyDto);
                }
            }
            return employeeHierarchyDto;
        }


    }
}
