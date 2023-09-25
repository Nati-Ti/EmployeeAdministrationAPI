using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Repositories;
using MediatR;


namespace EmployeeAdmin.Application.Handler
{
    public class DeleteAssignHandler : IRequestHandler<DeleteAssignCommand, String>
    {
        private readonly EmployeePositionRepository _repository;

        public DeleteAssignHandler(EmployeePositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<String> Handle(DeleteAssignCommand command, CancellationToken cancellationToken)
        {
            var inputId = command.id;
            var position = await _repository.GetEmployeePositionAsync(inputId);
            if (position == null)
            {
                throw new KeyNotFoundException("The specified position was not found.");
            }

            var children = await GetChildOfPositionId(inputId);
            var firstChild = children.OrderBy(c => c.Registered_On).First();
            if (children != null)
            {

                if (position.ParentId == null)
                {
                    firstChild.ParentId = null;
                }
                else
                {
                    firstChild.ParentId = position.ParentId;
                    firstChild.Description = position.Description;

                }
                await _repository.UpdateEmployeePositionAsync(firstChild);

                foreach (var child in children)
                {
                    if (child == firstChild)
                    {
                        continue;
                    }
                    else
                        child.ParentId = firstChild.Id;

                    await _repository.UpdateEmployeePositionAsync(child);
                }
            }
            await _repository.DeleteEmployeePositionAsync(position);

            return position.Name + " has been removed from the " + firstChild.Description + " position, and "
                + firstChild.Name + " has been assigned.";

        }

        public async Task<List<Employee>> GetChildOfPositionId(Guid id)
        {

            var allPositions = await _repository.GetAllEmployeePositionsAsync();
            var children = allPositions.Where(p => p.ParentId == id).ToList();

            return children;
        }



    }
}
