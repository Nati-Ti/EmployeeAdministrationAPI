using Microsoft.EntityFrameworkCore;
using EmployeeAdmin.Persistence.Data;
//using perago.Data;
using EmployeeAdmin.Domain.Model;


namespace EmployeeAdmin.Persistence.Repositories
{
    public class EmployeePositionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeePositionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeePositionsAsync()
        {
            return await _dbContext.EmployeePositions.ToListAsync();
        }

        public async Task<Employee?> GetRootPositionAsync()
        {
            return await _dbContext.EmployeePositions.SingleOrDefaultAsync(p => p.ParentId == null);
        }

        public async Task<Employee?> GetEmployeePositionAsync(Guid id)
        {
            return await _dbContext.EmployeePositions.FindAsync(id);
        }

        public async Task<List<Employee>> GetChildOfPositionAsync(Guid parentId)
        {
            return await _dbContext.EmployeePositions.Where(p => p.ParentId == parentId).ToListAsync();
        }

        public async Task<Employee> CreateEmployeePositionAsync(Employee employeePosition)
        {
            _dbContext.EmployeePositions.Add(employeePosition);
            await _dbContext.SaveChangesAsync();
            return employeePosition;
        }

        public async Task UpdateEmployeePositionAsync(Employee employeePosition)
        {
            _dbContext.EmployeePositions.Update(employeePosition);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeePositionAsync(Employee employeePosition)
        {
            _dbContext.EmployeePositions.Remove(employeePosition);
            await _dbContext.SaveChangesAsync();
        }
    }
}

