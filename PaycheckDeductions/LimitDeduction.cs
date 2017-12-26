using System;
using Domain.Interfaces;

namespace PaycheckDeductions
{
    public class LimitDeduction : IDeduction
    {
        private readonly double _limit;
        private readonly double _tax;

        public LimitDeduction(double limit, double tax)
        {
            _limit = limit;
            _tax = tax;
        }
        public string Description { get; set; }

        public double ApplyTo(ref double value)
        {
            double deduction = 0;
            if (value <= _limit)
            {
                deduction = value * _tax;
                value = 0;
            }
            else
            {
                deduction = _limit * _tax;
                value = value - _limit;
            }

            return deduction;
        }
    }
}
