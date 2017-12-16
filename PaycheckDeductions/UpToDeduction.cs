using Domain.Interfaces;
using System;

namespace Deduction
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
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
