using System;
using Domain.Interfaces;

namespace Domain
{
    [Serializable]
    public class Employee: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public string Country { get; set; }

        public Employee()
        {
            Id = -1;
        }

        public static bool ValidateHourlyRate(string valueToCheck, out string errorMessage)
        {

            bool valid;
            try
            {
                double.Parse(valueToCheck);
                valid = true;
                errorMessage = null;
            }
            catch (FormatException e)
            {
                valid = false;
                errorMessage = "O valor informado para o campo \"Valor por hora\" é inválido.";
            }

            return valid;            
        }

        public static bool ValidateHoursWorked(string valueToCheck, out string errorMessage)
        {
            bool valid;
            try
            {
                int.Parse(valueToCheck);
                valid = true;
                errorMessage = null;
            }
            catch (FormatException e)
            {
                valid = false;
                errorMessage = "O valor informado para o campo \"Horas Trabalhadas\" é inválido.";
            }

            return valid;
        }

        public static bool ValidateId(string valueToCheck, out string errorMessage)
        {
            bool valid;
            try
            {
                int.Parse(valueToCheck);
                valid = true;
                errorMessage = null;
            }
            catch (FormatException e)
            {
                valid = false;
                errorMessage = "O valor informado para o campo \"Id\" é inválido.";
            }

            return valid;
        }

        public static bool ValidateCountry(string valueToCheck, out string errorMessage)
        {
            bool valid = valueToCheck.Length <= 30;
            errorMessage = valid ? null : "O País deve ter até 30 characteres.";


            return valid;
        }

        public static bool ValidateName(string valueToCheck, out string errorMessage)
        {
            bool valid = valueToCheck.Length <= 50;
            errorMessage = valid ? null : "O Nome deve ter até 50 characteres.";


            return valid;
        }

    }
}