using Domain.Interfaces;
using System;

namespace Deduction
{
    public class FlatDeduction: IDeduction
    {
        private double tax;

        public FlatDeduction(double tax)
        {
            this.tax = tax;
        }

        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double ApplyTo(ref double value)
        {
            return value * tax;
        }
    }
}
