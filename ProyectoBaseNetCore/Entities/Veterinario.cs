using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoBaseNetCore.Entities
{
    [Table("Veterinario", Schema = "CAT")]
    public class Veterinario : CrudEntities
    {
        [Key]
        public long Id { get; set; }
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Sede { get; set; }
        public string Rol { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

    }
}
