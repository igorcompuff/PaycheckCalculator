using System;
using Domain.Interfaces;

namespace PaycheckDeductions
{
    public class FixedPartDeduction : IDeduction
    {
        private double _fixedPartValue;
        private double _tax;
        public FixedPartDeduction(double stepAmount, double tax)
        {
            _fixedPartValue = stepAmount;
            _tax = tax;
        }
        public string Description { get; set; }

        public double ApplyTo(ref double value)
        {
            int numberOfSteps = (int)(value / _fixedPartValue);
            return _tax * _fixedPartValue * numberOfSteps;
        }
    }
}
