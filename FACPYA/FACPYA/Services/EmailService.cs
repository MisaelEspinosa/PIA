using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    public async Task EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        var mensaje = new MimeMessage();
        mensaje.From.Add(MailboxAddress.Parse("xrmissa@gmail.com"));  // cambia esto
        mensaje.To.Add(MailboxAddress.Parse(destinatario));
        mensaje.Subject = asunto;
        mensaje.Body = new TextPart("plain") { Text = cuerpo };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync("xrmissa@gmail.com", "B2nXguyi");
        await smtp.SendAsync(mensaje);
        await smtp.DisconnectAsync(true);
    }
}
