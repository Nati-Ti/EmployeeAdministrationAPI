using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAdmin.Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<Employee> ChildPositions { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Registered_On { get; set; }
    }

}


