using ConsoleComponents;
using Domain;
using Mvc.ViewModel;
using System;
using System.Collections.Generic;

namespace Mvc.View
{
    public class EmployeeView
    {
        public static EmployeeVm RequestDataToAdd()
        {
            EmployeeVm emp = new EmployeeVm();
            var form = new Form<EmployeeVm>();

            form.AddInput(new BindInputText<EmployeeVm>("Nome: ", emp, "Name", Employee.ValidateName));
            form.AddInput(new BindInputText<EmployeeVm>("Valor por hora: R$", emp, "HourlyRate", Employee.ValidateHourlyRate));
            form.AddInput(new BindInputText<EmployeeVm>("Horas trabalhadas: ", emp, "HoursWorked", Employee.ValidateHourlyRate));
            form.AddInput(new BindInputText<EmployeeVm>("País: ", emp, "Country", Employee.ValidateCountry));
            Console.WriteLine();
            form.Show();

            return emp;
        }

        public static bool RequestConfirmation(EmployeeVm emp)
        {
            ConsoleMessage.ShowMessage("Revise as informações abaixo sobre o funcionário.\n");
            ConsoleMessage.ShowMessage(emp.ToString());

            return ConsoleConfirmation.Show("Confirmar?");
        }

        private static void RequestAlteration(EmployeeVm emp, string text, string value, string property, InputText.InputValidator validator)
        {
            ConsoleMessage.ShowMessage(text + value);
            if (ConsoleConfirmation.Show("Alterar?"))
            {
                new BindInputText<EmployeeVm>(text, emp, property, validator).Show();
            }
        }
        public static void RequestAlteration(EmployeeVm emp)
        {
            RequestAlteration(emp, "Nome: ", emp.Name, "Name", Employee.ValidateName);
            RequestAlteration(emp, "Valor por hora: R$", emp.HourlyRate, "HourlyRate", Employee.ValidateHourlyRate);
            RequestAlteration(emp, "Horas trabalhadas: ", emp.HoursWorked, "HoursWorked", Employee.ValidateHoursWorked);
            RequestAlteration(emp, "País: ", emp.Country, "Country", Employee.ValidateCountry);
        }

        public static int RequestId()
        {
            return int.Parse(new InputText("Entre com o Id do funcionário a ser alterado:", Employee.ValidateId).Show());
        }

        public static void ListEmployees(IEnumerable<EmployeeVm> employees)
        {
            ConsoleMessage.ShowMessage("################### Lista de funcionários. ###################\n");
            foreach (var employee in employees)
            {
                ConsoleMessage.ShowMessage(employee.ToString());
            }

            ConsoleMessage.ShowMessage(string.Empty, true, "Pressione qualquer tecla para continuar...");
        }

        public static void ShowErrorMessage(string message)
        {
            ConsoleMessage.ShowMessage(message, true);
        }

        public static void ShowPaycheck(Paycheck paycheck)
        {
            ConsoleMessage.ShowMessage(paycheck.ToString(), true);
        }
    }
}