using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Repositories;
using MediatR;

namespace EmployeeAdmin.Application.Handler
{
    public class DeleteCascadeHandler : IRequestHandler<DeleteCascadeCommand, String>
    {
        private readonly EmployeePositionRepository _repository;

        public DeleteCascadeHandler(EmployeePositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<String> Handle(DeleteCascadeCommand command, CancellationToken cancellationToken)
        {
            var position = await _repository.GetEmployeePositionAsync(command.Id);
            if (position == null)
            {
                throw new KeyNotFoundException("The specified Department was not found.");
            }
            var positionsToDelete = await GetChildrenRecursively(command.Id);
            positionsToDelete.Reverse();
            positionsToDelete.Add(position);

            foreach (var positionToDelete in positionsToDelete)
            {
                await _repository.DeleteEmployeePositionAsync(positionToDelete);
            }
            return "The " + position.Description + " position has been removed, including its sub-positions.";

        }

        public async Task<List<Employee>> GetChildrenRecursively(Guid id)
        {
            var allPositions = await _repository.GetAllEmployeePositionsAsync();
            var positions = allPositions.Where(p => p.ParentId == id).ToList();

            var allChildren = new List<Employee>(positions);

            foreach (var child in positions)
            {
                var subChildren = await GetChildrenRecursively(child.Id);
                allChildren.AddRange(subChildren);
            }
            return allChildren;
        }




    }
}
