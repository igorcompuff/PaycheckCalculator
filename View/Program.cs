using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Components;
using View.ViewModel;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu menu = new ConsoleMenu(true);

            menu.OptionNotFoundMessage = "Opção inválida. Tente novamente.";
            menu.RequestOptionMessage = "Entre com a opção desejada";

            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("1", "Adicionar funcionário", () => AddEmployee()));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("2", "Alterar funcionário", () => Console.WriteLine("Alterar")));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("3", "Remover funcionário", () => Console.WriteLine("Remover")));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("4", "Listar funcionários", () => Console.WriteLine("Listar")));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("5", "Calcular olerite", () => Console.WriteLine("Calcular")));
            menu.AddExitOption(new ConsoleMenu.ConsoleMenuOption("6", "Sair"));

            menu.Show();
        }

        public static void AddEmployee()
        {
            EmployeeVm emp = new EmployeeVm();
            var form = new Form<EmployeeVm>();

            foreach(var prop in emp.GetType().GetProperties())
            {
                switch(prop.Name.ToLower())
                {
                    case "hourlyrate": form.AddInput(new InputText<EmployeeVm>(emp, prop, "Valor por hora: R$")); break;
                    case "hoursworked": form.AddInput(new InputText<EmployeeVm>(emp, prop, "Horas Trabalhadas: ")); break;
                    case "country": form.AddInput(new InputText<EmployeeVm>(emp, prop, "País: ")); break;
                }
            }

            form.Show();
            Console.ReadKey();
        }
    }
}
