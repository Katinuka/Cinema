using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;

namespace Cinema.BLL.EmailSender;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;


public class MailjetEmailSender
{
    private readonly string? _apiKey;
    private readonly string? _apiSecret;

    public MailjetEmailSender(IConfiguration configuration)
    {
        _apiKey = configuration["Mailjet:ApiKey"];
        _apiSecret = configuration["Mailjet:ApiSecret"];
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {

        MailjetClient client = new MailjetClient(_apiKey, _apiSecret);
        MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "yaroslavkuzenko02@gmail.com")
            .Property(Send.FromName, "Yaroslav Kuzenko")
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, "Dear passenger, welcome to Mailjet! May the delivery force be with you!")
            .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {
                    {"Email", toEmail}
                }
            });
        MailjetResponse response = await client.PostAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
        }
        else
        {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
        }
    }

}