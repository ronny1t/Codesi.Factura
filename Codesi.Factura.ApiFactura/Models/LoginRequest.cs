namespace Codesi.Factura.Api.Models
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Cedula { get; set; } = string.Empty;
    }
}