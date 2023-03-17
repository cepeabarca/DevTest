using DevTest.BackEnd.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevTest.BackEnd.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>?> Get();
        Task<Employee?> Get(int id);
        Task<Employee?> Create(Employee employee);
        Task<Employee?> Update(int id, Employee employee);
        Task<int?> Delete(int id);
        Task<List<Employee>?> GetByRFC(string rfc);
        Task<List<Employee>?> GetByBornDate(DateTime bornDate);

    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DevTestContext _context;
        private readonly ILogger _logger;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, DevTestContext context)
        {
            _context = context;
            _logger = logger;
        }

        async Task<Employee?> IEmployeeRepository.Create(Employee employee)
        {
            try
            {
                employee.ID = 0;
                var add = await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Create | Exception: {ex}");
            }

            return null;
        }

        async Task<int?> IEmployeeRepository.Delete(int id)
        {
            try
            {
                var selectedEmployee = await _context.Employees.FindAsync(id);
                if (selectedEmployee != null)
                {
                    _context.Remove(selectedEmployee);
                    await _context.SaveChangesAsync();
                    return id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Delete | Exception: {ex}");
            }

            return null;
        }

        async Task<List<Employee>?> IEmployeeRepository.Get()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Get | Exception: {ex}");
            }

            return null;
        }

        async Task<List<Employee>?> IEmployeeRepository.GetByRFC(string rfc)
        {
            try
            {
                return await _context.Employees.Where(x => x.RFC.Contains(rfc) ).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Get | Exception: {ex}");
            }

            return null;
        }

        async Task<Employee?> IEmployeeRepository.Get(int id)
        {
            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Get(id) | Exception: {ex}");
            }

            return null;
        }

        async Task<Employee?> IEmployeeRepository.Update(int id, Employee newEmployee)
        {
            try
            {
                if (id != newEmployee.ID)
                {
                    _logger.LogError($"Los Identificadores no coinciden");
                    return null;
                }

                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return null;
                }

                employee.Name = newEmployee.Name;
                employee.LastName = newEmployee.LastName;
                employee.RFC = newEmployee.RFC;
                employee.Status = newEmployee.Status;

                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Update | Exception: {ex}");
                return null;
            }
        }

        async Task<List<Employee>?> IEmployeeRepository.GetByBornDate(DateTime bornDate)
        {
            try
            {
                return await _context.Employees.Where(x => x.BornDate == bornDate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on Get | Exception: {ex}");
            }

            return null;
        }
    }
}