using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoBaseNetCore.Entities
{
    [Table("Razas", Schema = "CAT")]
    public class Razas: CrudEntities
    {
        [Key]
        public long IdRaza { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Mascota> Mascotas { get; set; }

    }
}
