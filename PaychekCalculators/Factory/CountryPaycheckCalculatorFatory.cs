using Domain;
using PaychekCalculators.Concrete;

namespace PaychekCalculators.Factory
{
    public abstract class CountryPaycheckCalculatorFatory
    {
        public static CountryPaycheckCalculator GetPaycheckCalculator(Employee employee)
        {
            CountryPaycheckCalculator calculator = null;

            switch (employee.Country.ToLower())
            {
                case "ireland": calculator = new IrelandPayCheckCalculator(); break;
                case "italy": calculator = new ItalyPayCheckCalculator(); break;
                case "germany": calculator = new GermanyPayCheckCalculator(); break;
            }

            return calculator;
        }
    }
}
