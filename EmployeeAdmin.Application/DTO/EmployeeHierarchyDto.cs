namespace EmployeeAdmin.Application.DTO
{
    public class EmployeeHierarchyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public List<EmployeeHierarchyDto> ChildPositions { get; set; }
    }
}
