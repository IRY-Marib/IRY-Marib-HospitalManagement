using Microsoft.AspNetCore.Mvc;
using HospitalManagement.Models;
using HospitalManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

public class EmergencyController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public EmergencyController(ApplicationDbContext context,IConfiguration configuration )
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> AddNews ()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNews(Emergency news)
    {
        //if (ModelState.IsValid)
        //{
        //    _context.Emergencies.Add(news);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");
        //}
        //return View(news);

        if (ModelState.IsValid)
        {
            _context.Emergencies.Add(news);
            await _context.SaveChangesAsync();

            // Send email to all users
            var emailService = new EmailService(_configuration);
            var users = _context.Users.Select(u => u.Email).ToList();

            foreach (var userEmail in users)
            {
                emailService.SendEmail(userEmail, news.Title, news.Content);
            }

            return RedirectToAction("Index", "Home");
        }
        return View(news);
    }

    [HttpGet]
    public async Task<IActionResult> News()
    {
        var newsList = await _context.Emergencies
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
        return View(newsList);
    }



}

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SendEmail(string to, string subject, string body)
    {
        try
        {
            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["Smtp:Username"],
                    _configuration["Smtp:Password"]
                ),
                EnableSsl = true, // Required for TLS encryption
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true, // Use true for HTML content
            };

            mailMessage.To.Add(to);

            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email send failed: {ex.Message}");
            throw; // Optionally rethrow to log or handle higher up
        }
    }


    //public void SendEmail(string to, string subject, string body)
    //{
    //    var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
    //    {
    //        Port = int.Parse(_configuration["Smtp:Port"]),
    //        Credentials = new NetworkCredential(
    //            _configuration["Smtp:Username"],
    //            _configuration["Smtp:Password"]
    //        ),
    //        EnableSsl = true,
    //    };

    //    var mailMessage = new MailMessage
    //    {
    //        From = new MailAddress(_configuration["Smtp:From"]),
    //        Subject = subject,
    //        Body = body,
    //        IsBodyHtml = true,
    //    };
    //    mailMessage.To.Add(to);

    //    smtpClient.Send(mailMessage);
    //}
}
