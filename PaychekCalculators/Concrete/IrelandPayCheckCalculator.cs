using Deduction;

namespace Calculator.Concrete
{
    public class IrelandPayCheckCalculator: CountryPaycheckCalculator
    {
        public IrelandPayCheckCalculator()
        {
            AddIncomeDeduction();
            AddSocialChargeDeduction();
            AddPensionDeduction();
        }

        private void AddIncomeDeduction()
        {
            var incomeDeduction = new ListDeduction();

            incomeDeduction.AddDeduction(new UpToDeduction(600, 0.25));
            incomeDeduction.AddDeduction(new FlatDeduction(0.4));
            incomeDeduction.Description = "Income Tax: 25% for the first €600 and 40% thereafter";
        }

        private void AddSocialChargeDeduction()
        {
            var socialDeduction = new ListDeduction();

            socialDeduction.AddDeduction(new UpToDeduction(500, 0.07));
            socialDeduction.AddDeduction(new FlatDeduction(0.08));
            socialDeduction.Description = "Universal Social Charge: 7% applied to the first €500 euro and 8% thereafter";

            AddDeduction(socialDeduction);
        }

        private void AddPensionDeduction()
        {
            var pensionDeduction = new FlatDeduction(0.04);
            pensionDeduction.Description = "Compulsory Pension Contribution: 4% applied to the gross salary";

            AddDeduction(pensionDeduction);
        }
}