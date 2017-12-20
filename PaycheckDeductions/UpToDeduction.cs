using System;
using Domain.Interfaces;

namespace PaycheckDeductions
{
    public class UpToDeduction : IDeduction
    {
        private double limit;
        private double tax;

        public UpToDeduction(double limit, double tax)
        {
            this.limit = limit;
            this.tax = tax;
        }
        public string Description { get; set; }

        public double ApplyTo(ref double value)
        {
            double deduction = 0;
            if (value <= limit)
            {
                deduction = value * tax;
                value = 0;
            }
            else
            {
                deduction = limit * tax;
                value = value - limit;
            }

            return deduction;
        }
    }
}
