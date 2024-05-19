using Cinema.BLL.EmailSender;

namespace Cinema.Backend.Controllers;
using Cinema.DAL;
using Cinema.DAL.Implemantations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = SD.Admin)]

public class EmailSenderController: ControllerBase
{
    private readonly MailjetEmailSender _emailSender;

    public EmailSenderController(MailjetEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail(string toEmail, string subject, string body)
    {
        if (string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
        {
            return BadRequest("Email, subject, and body are required.");
        }

        try
        {
            await _emailSender.SendEmailAsync(toEmail, subject, body);
            return Ok("Email sent successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid argument: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, $"Operation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while sending email: {ex.Message}");
        }
    }
}