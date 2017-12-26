using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Domain;
using Domain.Interfaces;

namespace DataAccess
{
    public class FileStreamEmployeeRepository : BaseFileStreamRepository<Employee>
    {
        public FileStreamEmployeeRepository(string rootDirPath): base(rootDirPath)
        {

        }

        public override IEnumerable<Employee> GetAll()
        {
            var employees = base.GetAll();
            
            return employees.OrderBy(emp => emp.Id).ToList();
        }

        protected override Employee GetEntity(FileStream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            return  DecodeEmployee(buffer);
        }

        protected override void Save(FileStream fs, Employee obj)
        {
            StringBuilder recordBuilder = new StringBuilder();

            recordBuilder.Append($"{obj.Id}//");
            recordBuilder.Append($"{obj.Name}//");
            recordBuilder.Append($"{obj.Country}//");
            recordBuilder.Append($"{obj.HourlyRate}//");
            recordBuilder.Append($"{obj.HoursWorked}");

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(recordBuilder.ToString());
            fs.Write(bytes, 0, bytes.Length);
        }

        private Employee DecodeEmployee(byte[] encodedEmployee)
        {
            Employee employee = new Employee();
            var encoding = new UTF8Encoding();
            string[] fields = encoding.GetString(encodedEmployee).Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                employee.Id = int.Parse(fields[0]);
                employee.Name = fields[1];
                employee.Country = fields[2];
                employee.HourlyRate = double.Parse(fields[3]);
                employee.HoursWorked = int.Parse(fields[4]);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("The database is corrupted", e);
            }

            return employee;
        }
    }
}