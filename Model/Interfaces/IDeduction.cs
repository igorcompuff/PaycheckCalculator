namespace Domain.Interfaces
{
    public interface IDeduction
    {
        string Description { get; set; }
        double ApplyTo(ref double value);
    }
}