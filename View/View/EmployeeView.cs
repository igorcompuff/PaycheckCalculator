using ConsoleComponents;
using Domain;
using Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.View
{
    public class EmployeeView
    {
        public static EmployeeVm ShowAddEmployeeView()
        {
            EmployeeVm employee = new EmployeeVm();
            var form = new Form();

            form.AddComponent(new BindInputText<EmployeeVm>("Nome: ", employee, "Name", Employee.ValidateName));
            form.AddComponent(new BindInputText<EmployeeVm>("Valor por hora: R$", employee, "HourlyRate", Employee.ValidateHourlyRate));
            form.AddComponent(new BindInputText<EmployeeVm>("Horas trabalhadas: ", employee, "HoursWorked", Employee.ValidateHoursWorked));
            form.AddComponent(new BindInputText<EmployeeVm>("País: ", employee, "Country", Employee.ValidateCountry));
            Console.WriteLine();
            form.Show();

            var confirmation = new ConsoleConfirmation("\nRevise as informações abaixo sobre o funcionário.\n\n" + employee + "\nConfirmar?");
            confirmation.Show();

            if (!confirmation.Confirmed)
            {
                employee = null;
            }

            return employee;
        }

        public static bool ShowAlterEmployeeView(EmployeeVm employee)
        {
            RequestAlteration(employee, "Nome: ", employee.Name, "Name", Employee.ValidateName);
            RequestAlteration(employee, "Valor por hora: R$", employee.HourlyRate, "HourlyRate", Employee.ValidateHourlyRate);
            RequestAlteration(employee, "Horas trabalhadas: ", employee.HoursWorked, "HoursWorked", Employee.ValidateHoursWorked);
            RequestAlteration(employee, "País: ", employee.Country, "Country", Employee.ValidateCountry);

            var confirmation = new ConsoleConfirmation("\nRevise as informações abaixo sobre o funcionário.\n\n" + employee + "\nConfirmar?");
            confirmation.Show();

            return confirmation.Confirmed;
        }

        private static void RequestAlteration(EmployeeVm emp, string text, string value, string property, InputText.InputValidator validator)
        {
            var conMessage = new ConsoleMessage(text + value);
            conMessage.Show();

            var confirmation = new ConsoleConfirmation("Alterar?");
            confirmation.Show();
            if (confirmation.Confirmed)
            {
                new BindInputText<EmployeeVm>(text, emp, property, validator).Show();
            }
        }

        public static int RequestId()
        {
            var input = new InputText("Entre com o Id do funcionário a ser alterado:", Employee.ValidateId);
            input.Show();
            return int.Parse(input.InputData);
        }

        public static void ListEmployees(IEnumerable<EmployeeVm> employees)
        {
            var builder = new StringBuilder();

            builder.Append("################### Lista de funcionários. ###################\n");

            foreach (var employee in employees)
            {
                builder.Append(employee);
                builder.Append("\n");
            }

            var conMessage = new ConsoleMessage(builder.ToString(), true);
            conMessage.Show();
        }

        public static void ShowMessage(string message)
        {
            var conMessage = new ConsoleMessage(message, true);
            conMessage.Show();
        }

        public static void ShowPaycheck(Paycheck paycheck)
        {
            var conMessage = new ConsoleMessage(paycheck.ToString(), true);
            conMessage.Show();
        }
    }
}