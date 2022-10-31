namespace DapperPollyExample.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public DateTime Date { get; set; }
    public double EstimatedDuration { get; set; }
    public string ServiceDescription { get; set; }
    public double  EstimatedPrice { get; set; }

    public Appointment()
    {
        Id = Guid.NewGuid();
    }
}