using Microsoft.AspNetCore.Mvc;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    [Route("send-email")]
    public void SendEmail()
    {
        _emailService.SendEmail();
    }
}