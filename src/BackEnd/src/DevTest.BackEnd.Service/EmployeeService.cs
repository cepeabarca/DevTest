using Azure;
using DevTest.BackEnd.Data.Models;
using DevTest.BackEnd.Data.Repositories;

namespace DevTest.BackEnd.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>?> Get();
        Task<Employee?> Get(int id);
        Task<Employee?> Create(Employee employee);
        Task<Employee?> Update(int id, Employee employee);
        Task<int?> Delete(int id);
        Task<IEnumerable<Employee>?> GetByRFC(string rfc);
        Task<bool> IsNewRFC(string rfc);
        Task<IEnumerable<Employee>?> GetByBornDate(DateTime bornDate);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo; 
        public EmployeeService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        async Task<Employee?> IEmployeeService.Create(Employee employee)
        {
            return await _employeeRepo.Create(employee);
        }

        async Task<int?> IEmployeeService.Delete(int id)
        {
            return await _employeeRepo.Delete(id);
        }

        async Task<IEnumerable<Employee>?> IEmployeeService.Get()
        {
            return await _employeeRepo.Get();
        }

        async Task<Employee?> IEmployeeService.Get(int id)
        {
            return await _employeeRepo.Get(id);
        }

        async Task<bool> IEmployeeService.IsNewRFC(string rfc)
        {
            var result = await _employeeRepo.GetByRFC(rfc);
            return result != null && result.Any() ? false : true;
        }

        async Task<IEnumerable<Employee>?> IEmployeeService.GetByRFC(string rfc)
        {
            return await _employeeRepo.GetByRFC(rfc);
        }

        async Task<IEnumerable<Employee>?> IEmployeeService.GetByBornDate(DateTime bornDate)
        {
            return await _employeeRepo.GetByBornDate(bornDate);
        }

        async Task<Employee?> IEmployeeService.Update(int id, Employee employee)
        {
            return await _employeeRepo.Update(id, employee);
        }
    }
}