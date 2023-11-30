using BlazorFullStackCrudWASM.Server.Models;
using BlazorFullStackCrudWASM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrudWASM.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;   
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees() 
        {
            var emps= _context.Employees.ToList();
            if (emps != null)
            {
                return Ok(emps);
            }
            else
                return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(i => i.Id == id);

            if (employee == null)
            {
                return NotFound(); // Employee with the given id not found
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (id != updatedEmployee.Id)
            {
                return BadRequest(); // Requested ID does not match the employee's ID
            }

            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(em => em.Id == updatedEmployee.Id);

            if (existingEmployee != null)
            {
                // Update the properties of the existing entity
                existingEmployee.FirstName = updatedEmployee.FirstName;
                existingEmployee.LastName = updatedEmployee.LastName;
                existingEmployee.Department = updatedEmployee.Department;
                existingEmployee.salary = updatedEmployee.salary;

                // Call SaveChanges to persist the changes
              await  _context.SaveChangesAsync();
            }
            return Ok(); // Successfully updated the employee
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); // Employee with the given id not found
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee); // Successfully deleted the employee
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> InsertEmployee(Employee newEmployee)
        {
            try
            {
                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                return Ok(newEmployee);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error occurred during employee insertion: {ex.Message}");
                throw; // You might handle the exception or rethrow it based on your error handling strategy
            }
        }



    }
}
