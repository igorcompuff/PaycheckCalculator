using System;
using Domain.Interfaces;

namespace PaycheckDeductions
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
        public string Description { get; set; }

        public double ApplyTo(ref double value)
        {
            int numberOfSteps = (int)(value / stepAmount);
            return tax * stepAmount * numberOfSteps;
        }
    }
}
