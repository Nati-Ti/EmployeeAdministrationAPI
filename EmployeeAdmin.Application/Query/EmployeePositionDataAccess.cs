using EmployeeAdmin.Domain.Model;
using EmployeeAdmin.Persistence.Context;
using EmployeeAdmin.Application.DTO;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace EmployeeAdmin.Application.Query
{
    public class EmployeePositionDataAccess
    {
        private readonly DapperContext _context;

        public EmployeePositionDataAccess(DapperContext context)
        {
            _context = context;

        }


        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var query = "SELECT * FROM EmployeePositions";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);

                return employees;
            }
        }

        public async Task<IEnumerable<EmployeePositionDto>> GetIdNameEmployees()
        {
            var query = "SELECT Id, Name FROM EmployeePositions";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeePositionDto>(query);

                return employees;
            }
        }

        public async Task<IEnumerable<EmployeePositionInputDto>> GetInputEmployees()
        {
            var query = "SELECT Id, Name, Description, ParentId FROM EmployeePositions";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeePositionInputDto>(query);

                return employees;
            }
        }

        public async Task<IEnumerable<EmployeeHierarchyDto>> GetHierarchyEmployees()
        {
            var query = "SELECT Id, Name, Description, ParentId FROM EmployeePositions";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<EmployeeHierarchyDto>(query);

                return employees;
            }
        }



        //Get Employees by Id

        public async Task<Employee> GetAllEmployeeId(Guid id)
        {
            var query = "SELECT * FROM EmployeePositions WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { Id = id });

                return employee;
            }
        }

        public async Task<EmployeePositionDto> GetIdNameEmployeeId(Guid id)
        {
            var query = "SELECT Id, Name FROM EmployeePositions WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeePositionDto>(query, new { Id = id });

                return employee;
            }
        }

        public async Task<EmployeePositionInputDto> GetInputEmployeeId(Guid id)
        {
            var query = "SELECT Id, Name, Description, ParentId FROM EmployeePositions WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeePositionInputDto>(query, new { Id = id });

                return employee;
            }
        }

        public async Task<EmployeeChildDto> GetChildrenOfEmployee(Guid id)
        {
            var query = "SELECT Id, Name, Description FROM EmployeePositions WHERE ParentId = @id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeeChildDto>(query, new { ParentId = id });

                return employee;
            }
        }
        

        public async Task<EmployeeHierarchyDto> GetHierarchyEmployeeId(Guid id)
        {
            var query = "SELECT Id, Name, Description, ParentId FROM EmployeePositions WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeeHierarchyDto>(query, new { Id = id });

                return employee;
            }
        }






    }
}