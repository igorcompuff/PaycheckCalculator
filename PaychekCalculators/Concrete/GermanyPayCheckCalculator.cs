using PaycheckDeductions;

namespace PaychekCalculators.Concrete
{
    public class GermanyPayCheckCalculator: CountryPaycheckCalculator
    {
        public GermanyPayCheckCalculator()
        {
            AddIncomeDeduction();
            AddPensionDeduction();
        }
        private void AddIncomeDeduction()
        {
            var incomeDeduction = new ListDeduction();

            incomeDeduction.AddDeduction(new LimitDeduction(400, 0.25));
            incomeDeduction.AddDeduction(new FlatDeduction(0.32));
            incomeDeduction.Description = "Income Tax: 25% for the first €400 and 32% thereafter";

            AddDeduction(incomeDeduction);
        }

        private void AddPensionDeduction()
        {
            var pensionDeduction = new FlatDeduction(0.02);
            pensionDeduction.Description = "Compulsory Pension Contribution: 2% applied to the gross salary";

            AddDeduction(pensionDeduction);
        }
    }
}