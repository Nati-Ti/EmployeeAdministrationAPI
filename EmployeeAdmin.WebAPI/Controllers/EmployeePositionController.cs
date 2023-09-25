using Microsoft.AspNetCore.Mvc;
using EmployeeAdmin.Application.DTO;
using EmployeeAdmin.Application.Queries;
using EmployeeAdmin.Application.Command;
using EmployeeAdmin.Application.Query;
using MediatR;

namespace EmployeeAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePositionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly EmployeePositionDataAccess _dataAccess;
        public EmployeePositionsController(IMediator mediator, EmployeePositionDataAccess dataAccess)
        {
            _mediator = mediator;
            _dataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var Employees = await _dataAccess.GetAllEmployees();
                //var Employees = await _mediator.Send(new GetAllEmployeesQuery());
                //var Employees = await _service.GetEmployees();
                return Ok(Employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePositionInputDto>> GetEmployeeId(Guid id)
        {
            try
            {
                var Employee = await _dataAccess.GetInputEmployeeId(id);
                //var Employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
                //var Employees = await _service.GetEmployeeById(id);
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeePositionInputDto>> CreatePosition(EmployeePositionInputDto inputDto)
        {
            try
            {
                var employeePosition = await _mediator.Send(new CreatePositionCommand(inputDto));
                //var employeePosition = await _service.CreatePositionAsync(inputDto);
                return CreatedAtAction(nameof(GetEmployeeId), new { id = employeePosition.Id }, employeePosition);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeePositionInputDto>> UpdatePosition(Guid id, EmployeePositionInputDto inputDto)
        {
            try
            {
                var updatedEmployee = await _mediator.Send(new UpdatePositionCommand(id, inputDto));
                //await _service.UpdatePositionAsync(id, inputDto);
                return Ok(updatedEmployee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("hierarchy")]
        public async Task<ActionResult<IEnumerable<EmployeeHierarchyDto>>> GetPositionHierarchy()
        {
            var positionHierarchy = await _mediator.Send(new HierarchyQuery());

            return Ok(positionHierarchy);
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<IEnumerable<EmployeePositionDto>>> GetChildrenOfPosition(Guid id)
        {
            try
            {
                var childPositions = await _dataAccess.GetChildrenOfEmployee(id);
                //var childPositions = await _mediator.Send(new GetChildrenByIdQuery(id));
                return Ok(childPositions);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/assign")]
        public async Task<ActionResult<String>> DeleteAssignPosition(Guid id)
        {
            try
            {
                var confirmation = await _mediator.Send(new DeleteAssignCommand(id));
                return confirmation;
                // await _service.DeleteAssignPositionAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }


        }

        [HttpDelete("{id}/Cascade")]
        public async Task<ActionResult<String>> DeleteCascadedPosition(Guid id)
        {
            try
            {
                var confirmation = await _mediator.Send(new DeleteCascadeCommand(id));
                return confirmation;
                //await _service.DeleteCascadedPositionAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}