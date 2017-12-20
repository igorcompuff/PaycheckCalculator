using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;

namespace DataAccess
{
    public class MemoryEmployeeRepository : IRepository<Employee>
    {
        List<Employee> employees = new List<Employee>();
        private static int seqNumber = 0;
        public void Add(Employee employee)
        {
            if (employee.Id < 0)
            {
                employee.Id = seqNumber++;
                employees.Add(employee);       
            }
            else
            {
                Employee employeeToAlter = employees.First(emp => emp.Id == employee.Id);

                if (employeeToAlter != null)
                {
                    employeeToAlter.Country = employee.Country;
                    employeeToAlter.HourlyRate = employee.HourlyRate;
                    employeeToAlter.HoursWorked = employee.HoursWorked;
                }
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            var list = new List<Employee>();
            list.AddRange(employees);
            return list;
        }

        public void Remove(Employee obj)
        {
            employees.Remove(obj);
        }

        public Employee GetById(int id)
        {
            return employees.FirstOrDefault(emp => emp.Id == id);
        }
    }
}
