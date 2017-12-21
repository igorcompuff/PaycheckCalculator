using System;
using System.Collections.Generic;
using Domain;
using Domain.Interfaces;
using Mvc.View;
using Mvc.ViewModel;
using PaychekCalculators;
using PaychekCalculators.Factory;

namespace Mvc.Controller
{
    public class EmployeeController
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeController(IRepository<Employee> repository)
        {
            _repository = repository;
        }
        public void AddEmployee()
        {
            EmployeeVm employee = EmployeeView.ShowAddEmployeeView();

            if (employee != null)
            {
                var domainEmployee = new Employee(){Name = employee.Name, Country = employee.Country, HoursWorked = int.Parse(employee.HoursWorked), HourlyRate = double.Parse(employee.HourlyRate)};

                try
                {
                    _repository.Add(domainEmployee);
                    EmployeeView.ShowMessage("Funcionário adicionado com sucesso.");
                }
                catch (Exception e)
                {
                    EmployeeView.ShowMessage("Erro ao adicionar o funcionário.");
                }
            }
        }

        public void AlterEmployee()
        {
            int id = EmployeeView.RequestId();
            Employee employee = _repository.GetById(id);

            if (employee != null)
            {
                EmployeeVm employeeVm = new EmployeeVm(){Name = employee.Name, Country = employee.Country, HourlyRate = employee.HourlyRate.ToString(), HoursWorked = employee.HoursWorked.ToString()};
                
                if (EmployeeView.ShowAlterEmployeeView(employeeVm))
                {
                    employee.Name = employeeVm.Name;
                    employee.Country = employeeVm.Country;
                    employee.HourlyRate = double.Parse(employeeVm.HourlyRate);
                    employee.HoursWorked = int.Parse(employeeVm.HoursWorked);

                    try
                    {
                        _repository.Add(employee);
                        EmployeeView.ShowMessage("Funcionário alterado com sucesso.");
                    }
                    catch (Exception e)
                    {
                        EmployeeView.ShowMessage("Erro ao alterar o Funcionário.");
                    }
                }
            }
            else
            {
                EmployeeView.ShowMessage("Funcionário não encontrado.");
            }
        }

        public void ListEmployees()
        {
            var domainEmployees = _repository.GetAll();
            var employeesVm = new List<EmployeeVm>();

            foreach (var employee in domainEmployees)
            {
                employeesVm.Add(new EmployeeVm(){Id = employee.Id, Name = employee.Name, Country = employee.Country, HourlyRate = employee.HourlyRate.ToString(), HoursWorked = employee.HoursWorked.ToString()});
            }

            EmployeeView.ListEmployees(employeesVm);
        }

        public void RemoveEmployee()
        {
            int id = EmployeeView.RequestId();
            Employee employee = _repository.GetById(id);

            if (employee != null)
            {
                _repository.Remove(employee);
                EmployeeView.ShowMessage("Funcionário removido com sucesso");
            }
            else
            {
                EmployeeView.ShowMessage("Funcionário não encontrado.");
            }
        }

        public void CalculatePaycheck()
        {
            int id = EmployeeView.RequestId();
            Employee employee = _repository.GetById(id);

            if (employee != null)
            {
                CountryPaycheckCalculator calculator = CountryPaycheckCalculatorFatory.GetPaycheckCalculator(employee);

                if (calculator != null)
                {
                    Paycheck paycheck = calculator.CalculatePayCheck(employee);
                    EmployeeView.ShowPaycheck(paycheck);
                }
                else
                {
                    EmployeeView.ShowMessage($"O país {employee.Country} não é suportado para cálculo de olerite.");
                }
            }
            else
            {
                EmployeeView.ShowMessage("Funcionário não encontrado.");
            }
        }
    }
}
