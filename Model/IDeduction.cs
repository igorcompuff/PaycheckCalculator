namespace Model
{
    public interface IDeduction
    {
        string Description { get; set; }
        double ApplyTo(double value);
    }
}