using System.Collections.Generic;

namespace Domain
{
    public class Paycheck
    {
        private readonly List<string> mDeductions = new List<string>();
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }

        public void AddDeduction(string deduction)
        {
            mDeductions.Add(deduction);
        }

        public void Print(IPrinter printer)
        {
            printer.Print($"Gross Salary = {GrossSalary}");

            foreach (var deduction in mDeductions)
            {
                printer.Print(deduction);
            }

            printer.Print($"Net Salary = {NetSalary}");
        }
    }
}