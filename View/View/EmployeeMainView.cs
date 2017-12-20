using ConsoleComponents;
using DataAccess;
using Mvc.Controller;
using System;

namespace Mvc.View
{
    public class EmployeeMainView
    {
        private readonly EmployeeController _controller;

        public EmployeeMainView(EmployeeController controller)
        {
            _controller = controller;
        }
        public void ShowMenu()
        {
            ConsoleMenu menu = new ConsoleMenu(true) { OptionNotFoundMessage = "Opção inválida. Tente novamente.", RequestOptionMessage = "Entre com a opção desejada" };

            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("1", "Adicionar funcionário", () => _controller.AddEmployee()));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("2", "Alterar funcionário", () => _controller.AlterEmployee()));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("3", "Remover funcionário", () => _controller.RemoveEmployee()));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("4", "Listar funcionários", () => _controller.ListEmployees()));
            menu.AddOption(new ConsoleMenu.ConsoleMenuOption("5", "Calcular olerite", () => _controller.CalculatePaycheck()));
            menu.AddExitOption(new ConsoleMenu.ConsoleMenuOption("6", "Sair"));

            menu.Show();
        }

        static void Main(string[] args)
        {
            new EmployeeMainView(new EmployeeController(new SerializationFileStreamEmployeeRepository(@"\\Mac\Home\Desktop"))).ShowMenu();
        }
    }
}
