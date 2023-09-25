using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Repositories;
using MediatR;
using EmployeeAdmin.Application.DTO;



namespace EmployeeAdmin.Application.Handler
{
    public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, EmployeePositionInputDto>
    {
        private readonly EmployeePositionRepository _repository;

        public UpdatePositionHandler(EmployeePositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeePositionInputDto> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
        {
            var inputDto = command.inputDto;
            var inputId = command.id;
            var allPositions = await _repository.GetAllEmployeePositionsAsync();

            var position = allPositions.FirstOrDefault(p => p.Id == inputId);

            if (position == null)
            {
                throw new KeyNotFoundException("The specified position was not found.");
            }

            var employeePosition = new Employee
            {
                Id = inputId,
                Name = inputDto.Name,
                Description = inputDto.Description,
                ParentId = inputDto.ParentId
            };

            if (employeePosition.ParentId != null && !allPositions.Any(p => p.Id == employeePosition.ParentId))
            {
                throw new InvalidOperationException("Invalid ParentId. The specified parent position does not exist.");
            }

            await _repository.UpdateEmployeePositionAsync(employeePosition);

            inputDto.Id = inputId;
            return inputDto;
        }
    }
}
