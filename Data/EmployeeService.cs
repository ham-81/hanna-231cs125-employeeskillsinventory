
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Employee_Skill_Inventory.Data
{
    public class EmployeeService : IEmployeeService
    {
        public readonly SqlConnectionConfiguration _configuration;

        public EmployeeService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateEmployee(Employee employee)
        {
            const string query = @"insert into dbo.EmployeeSkill(EmployeeName,Skill,SkillLevel,LastUpdated) values (@EmployeeName,@Skill,@SkillLevel,@LastUpdated)";

            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { employee.EmployeeName, employee.Skill, employee.SkillLevel, employee.LastUpdated }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"delete from dbo.EmployeeSkill where EmployeeId=@Id";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
            return true;
        }

        public async Task<bool> EditEmployee(int id, Employee employee)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"update dbo.EmployeeSkill set EmployeeName=@EmployeeName,Skill=@Skill,SkillLevel=@SkillLevel,LastUpdated=@LastUpdated where EmployeeID=@id";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { employee.EmployeeName, employee.Skill, employee.SkillLevel, employee.LastUpdated, id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees;
            const string query = @"select * from dbo.EmployeeSkill";
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employees = (await conn.QueryAsync<Employee>(query)).ToList();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
            return employees;
        }

        public async Task<Employee> SingleEmployee(int id)
        {
            Employee employee = new Employee();
            const string query = @"select * from dbo.EmployeeSkill where EmployeeID=@id";
            using (var conn = new SqlConnection(_configuration.Value))
            {
                try
                {
                    employee = await conn.QueryFirstOrDefaultAsync<Employee>(query, new { id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return employee;
        }
    }
}
