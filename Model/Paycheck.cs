using System.Collections.Generic;

namespace Model
{
    public class Paycheck
    {
        private readonly List<string> mDeductions = new List<string>();
        public double GrossSalary { get; private set; }
        public double NetSalary { get; private set; }

        public Paycheck(double gross, double net)
        {
            GrossSalary = gross;
            NetSalary = net;
        }

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