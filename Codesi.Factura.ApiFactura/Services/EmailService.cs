using MailKit.Security;
using MimeKit;

namespace Codesi.Factura.Api.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarCorreoAsync(
            string destinatario,
            string asunto,
            string mensaje)
        {
            var host =
                _configuration["Email:Host"];

            var portTexto =
                _configuration["Email:Port"];

            var usuario =
                _configuration["Email:User"];

            var password =
                _configuration["Email:Password"];

            var nombreRemitente =
                _configuration["Email:FromName"];

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new Exception(
                    "No está configurado Email:Host."
                );
            }

            if (!int.TryParse(
                    portTexto,
                    out int port))
            {
                throw new Exception(
                    "Email:Port no es válido."
                );
            }

            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new Exception(
                    "No está configurado Email:User."
                );
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(
                    "No está configurado Email:Password."
                );
            }

            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    nombreRemitente ??
                        "Sistema Académico de Facturación",
                    usuario
                )
            );

            email.To.Add(
                MailboxAddress.Parse(destinatario)
            );

            email.Subject = asunto;

            email.Body = new TextPart("plain")
            {
                Text = mensaje
            };

            using var smtp =
                new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                host,
                port,
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                usuario,
                password
            );

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }

        // ==================================================
        // ENVIAR CORREO CON PDF ADJUNTO
        // ==================================================

        public async Task EnviarCorreoConPdfAsync(
            string destinatario,
            string asunto,
            string mensaje,
            byte[] pdf,
            string nombreArchivo)
        {
            var host =
                _configuration["Email:Host"];

            var portTexto =
                _configuration["Email:Port"];

            var usuario =
                _configuration["Email:User"];

            var password =
                _configuration["Email:Password"];

            var nombreRemitente =
                _configuration["Email:FromName"];

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new Exception(
                    "No está configurado Email:Host."
                );
            }

            if (!int.TryParse(
                    portTexto,
                    out int port))
            {
                throw new Exception(
                    "Email:Port no es válido."
                );
            }

            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new Exception(
                    "No está configurado Email:User."
                );
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(
                    "No está configurado Email:Password."
                );
            }

            if (pdf == null || pdf.Length == 0)
            {
                throw new Exception(
                    "El PDF de la factura está vacío."
                );
            }

            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    nombreRemitente ??
                        "Sistema Académico de Facturación",
                    usuario
                )
            );

            email.To.Add(
                MailboxAddress.Parse(destinatario)
            );

            email.Subject = asunto;

            var cuerpo = new BodyBuilder
            {
                TextBody = mensaje
            };

            cuerpo.Attachments.Add(
                nombreArchivo,
                pdf,
                new ContentType(
                    "application",
                    "pdf"
                )
            );

            email.Body = cuerpo.ToMessageBody();

            using var smtp =
                new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                host,
                port,
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                usuario,
                password
            );

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}