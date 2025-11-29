using EmployeesApi.Data;
using EmployeesApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _db;

        public EmployeeService(EmployeeDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            // Use AsNoTracking so EF doesn't cache Age incorrectly
            return await _db.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            var existing = await _db.Employees.FindAsync(employee.Id);
            if (existing == null) return null;

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.DateOfBirth = employee.DateOfBirth;
            existing.EducationLevel = employee.EducationLevel;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return false;

            _db.Employees.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
