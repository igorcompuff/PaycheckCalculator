using PaycheckDeductions;

namespace PaychekCalculators.Concrete
{
    public class ItalyPayCheckCalculator: CountryPaycheckCalculator
    {
        public ItalyPayCheckCalculator()
        {
            AddIncomeDeduction();
            AddInpsDeduction();
        }
        private void AddIncomeDeduction()
        {
            var incomeDeduction = new FlatDeduction(0.25);
            incomeDeduction.Description = "Income Tax: flat rate of 25% over the gross salary";
            AddDeduction(incomeDeduction);
        }

        private void AddInpsDeduction()
        {
            var inpsDeduction = new ListDeduction();
            inpsDeduction.AddDeduction(new LimitDeduction(500, 0.09));
            inpsDeduction.AddDeduction(new FixedPartDeduction(100, 0.001));
            inpsDeduction.Description = "INPS contribution applied on the gross salary. This is charged at 9% for the first €500 and increases by .1% over every €100 thereafter.";
            AddDeduction(inpsDeduction);
        }
    }
}