using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace Domain
{
    public class Paycheck
    {
        private readonly List<string> _deductions = new List<string>();
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }

        public void AddDeduction(string deduction)
        {
            _deductions.Add(deduction);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Gross Salary = {GrossSalary}\n");

            foreach (var deduction in _deductions)
            {
                builder.Append(deduction + "\n");
            }

            builder.Append($"Net Salary = {NetSalary}");

            return builder.ToString();
        }
    }
}