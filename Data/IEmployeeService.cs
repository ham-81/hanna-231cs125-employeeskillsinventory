namespace Employee_Skill_Inventory.Data
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(Employee employee);
        Task<List<Employee>> GetEmployees();
        Task<bool> EditEmployee(int id, Employee employee);
        Task<Employee> SingleEmployee(int id);
        Task<bool> DeleteEmployee(int id);
    }
}
