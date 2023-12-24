using System.ComponentModel.DataAnnotations;

namespace TMS_MANTENIMIENTO.API.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cedula { get; set; } = null;
        public bool IsClient { get; set; }=false;
    }
}