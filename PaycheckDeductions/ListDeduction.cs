using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Deduction
{
    public class ListDeduction: IDeduction
    {
        private List<IDeduction> mDeductions = new List<IDeduction>();

        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddDeduction(IDeduction deduction)
        {
            mDeductions.Add(deduction);
        }

        public double ApplyTo(ref double value)
        {
            double total = 0;

            foreach(var deduction in mDeductions)
            {
                total += deduction.ApplyTo(ref value);
            }

            return total;
        }
    }
}
