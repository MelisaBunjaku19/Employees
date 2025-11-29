namespace EmployeesApi.DTOs
{
    public class GetEmployeeDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? EducationLevel { get; set; }
    }
}