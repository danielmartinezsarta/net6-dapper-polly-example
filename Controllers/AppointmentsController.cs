using DapperPollyExample.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DapperPollyExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(ILogger<AppointmentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAppointmentList")]
    public IEnumerable<Appointment> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Appointment
            {
                Customer = new Customer
                {
                    Name = $"TestCustomer {index}", Phone = "123456789", Address = $"Test Address {index}",
                    Email = "email@example.com", TaxNumber = "123456-0"
                },
                Date = DateTime.Now,
                EstimatedDuration = index,
                EstimatedPrice = 50_000 * index,
                ServiceDescription = $"Test description {index}"
            })
            .ToArray();
    }
}