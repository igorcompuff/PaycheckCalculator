using System;
using Domain.Interfaces;

namespace PaycheckDeductions
{
    public class FlatDeduction: IDeduction
    {
        private double tax;

        public FlatDeduction(double tax)
        {
            this.tax = tax;
        }

        public string Description { get; set; }

        public double ApplyTo(ref double value)
        {
            return value * tax;
        }
    }
}
