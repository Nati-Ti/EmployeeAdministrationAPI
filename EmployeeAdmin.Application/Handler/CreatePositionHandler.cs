using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Repositories;
using EmployeeAdmin.Application.DTO;
using MediatR;


namespace EmployeeAdmin.Application.Handler
{
    public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, EmployeePositionInputDto>
    {
        private readonly EmployeePositionRepository _repository;

        public CreatePositionHandler(EmployeePositionRepository repository)
        {
            _repository = repository;
        }
        public async Task<EmployeePositionInputDto> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
        {
            var inputDto = command.InputDto;
            var employeePosition = new Employee
            {
                Id = Guid.NewGuid(),
                Name = inputDto.Name,
                Description = inputDto.Description,
                ParentId = inputDto.ParentId
            };

            var allPositions = await _repository.GetAllEmployeePositionsAsync();

            var existParent = allPositions.Any(p => p.Id == employeePosition.ParentId);

            if (employeePosition.ParentId != null && !existParent)
            {
                throw new InvalidOperationException("Invalid ParentId. The specified parent position does not exist.");
            }
            if (employeePosition.ParentId == null && allPositions.Any())
            {
                throw new InvalidOperationException("Two root positions cannot be created.");
            }

            var createdEmployeePosition = await _repository.CreateEmployeePositionAsync(employeePosition);

            inputDto.Id = employeePosition.Id;


            return inputDto;

        }



    }
}
