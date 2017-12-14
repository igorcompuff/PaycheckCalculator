using System.Collections.Generic;

namespace Model
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

        public Paycheck CalculatePayCheck(Employee emp)
        {
            double grossSalary = emp.HourlyRate * emp.HoursWorked;
            double netSalary = grossSalary - CalculateTotalDeductions();

            return new Paycheck(mDeductions, grossSalary, netSalary);
        }

        public abstract void CalculateTotalDeductions(Paycheck pc);
    }
}