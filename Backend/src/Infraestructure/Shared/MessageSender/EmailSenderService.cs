using System.Net;
using System.Net.Mail;

namespace Backend.Infrastructure.Shared.MessageSender;

public class EmailSenderService : IMessageSenderService {
    private readonly SmtpClient smtpClient = new("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("no.reply.hospitalg059@gmail.com","zyoc ssgs zlpt vycs"),
        EnableSsl = true,
    };
    public void SendMessage(string recipient, string subject, string body){
        MailMessage mailMessage = new("no.reply.hospitalg059@gmail.com", recipient, subject, body);
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }
}