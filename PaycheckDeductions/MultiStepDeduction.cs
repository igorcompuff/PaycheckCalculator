using Domain.Interfaces;
using System;

namespace Deduction
{
    public class MultiStepDeduction : IDeduction
    {
        private double stepAmount;
        private double tax;
        public MultiStepDeduction(double stepAmount, double tax)
        {
            this.stepAmount = stepAmount;
            this.tax = tax;
        }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double ApplyTo(ref double value)
        {
            int numberOfSteps = (int)(value / stepAmount);
            return tax * stepAmount * numberOfSteps;
        }
    }
}
