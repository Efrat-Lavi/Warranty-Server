
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Interfaces;
using Warranty.Core.Models;

public class WarrantyExpirationJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WarrantyExpirationJob> _logger;

    public WarrantyExpirationJob(IServiceProvider serviceProvider, ILogger<WarrantyExpirationJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // יצירת scope חדש עבור כל הריצה של job
            using (var scope = _serviceProvider.CreateScope())
            {
                // הזרקת שירותים מתוך ה-scope
                var repositoryManager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                DateTime today = DateTime.UtcNow.Date;
                DateTime alertDate = today.AddDays(30); // בדיקה של 30 יום לפני

                // גישה ל-repository דרך ה-Injected service
                List<RecordModel> expiringWarranties = await repositoryManager.recordRepository.GetRecordsByDate(alertDate);

                foreach (RecordModel record in expiringWarranties)
                {
                    var user = record.User;
                    if (user != null && user.IsAccessEmails)
                    {

                        string emailBody = $@"
<p>Hello {user.NameUser},</p>
<p>The warranty for <strong style='font-size: 18px; color: #2E8B57;'>{record.Warranty.NameProduct}</strong> is about to expire on <strong>{record.Warranty.ExpirationDate:dd/MM/yyyy}</strong>.</p>
<p>To extend the warranty, please click the button below:</p>
<a href='https://keepit-client-users.onrender.com/record/{record.Warranty.Id}' 
   style='display: inline-block; background-color: #10a37f; color: white; padding: 10px 20px; text-decoration: none; border-radius: 12px; font-weight: bold; text-align: center;'>
    Extend Warranty
</a>";


                        var emailRequest = new EmailRequest
                        {
                            To = user.Email,
                            Subject = "תזכורת: האחריות שלך עומדת לפוג",
                            Body = emailBody,
                            IsHtml = true
                        };

                        await emailService.SendEmailAsync(emailRequest);
                    }
                }
            }

            _logger.LogInformation("Job: בדיקת תוקף אחריות הסתיימה בהצלחה.");
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // להריץ כל 24 שעות
        }
    }
}

