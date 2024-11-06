namespace Backend.Infrastructure.Shared.MessageSender;

public interface IMessageSenderService{
    public void SendMessage(string recipient, string subject, string body);
}