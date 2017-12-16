using Domain;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Calculator
{
    public abstract class CountryPaycheckCalculator
    {
        private List<IDeduction> mDeductions = new List<IDeduction>();
        
        public void AddDeduction(IDeduction deduction)
        {
            mDeductions.Add(deduction);
        }

        public void RemoveDeduction(IDeduction deduction)
        {
            mDeductions.Remove(deduction);
        }

        public Paycheck CalculatePayCheck(Employee employee)
        {
            Paycheck payCheck = new Paycheck();
            payCheck.GrossSalary = employee.HourlyRate * employee.HoursWorked;
            CalculateNetSalary(payCheck);

            return payCheck;
        }

        public void CalculateNetSalary(Paycheck pc)
        {
            double totalDeductions = 0;
            double grossSalary = pc.GrossSalary;
            foreach (var deduction in mDeductions)
            {
                var deductionValue = deduction.ApplyTo(ref grossSalary);
                totalDeductions += deductionValue;
                pc.AddDeduction($"R$ {deductionValue} - {deduction.Description}");
            }

            pc.NetSalary = pc.GrossSalary - totalDeductions;
        }
    }
}