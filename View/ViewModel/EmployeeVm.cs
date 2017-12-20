namespace Mvc.ViewModel
{
    public class EmployeeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; internal set; }
        public string HoursWorked { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Name}\nValor por hora: R${HourlyRate}\nHoras Trabalhadas: {HoursWorked}\nPaís: {Country}\n";
        }
    }
}
