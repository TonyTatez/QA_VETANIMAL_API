using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VET_ANIMAL_API.Entities;

namespace ProyectoBaseNetCore.Entities
{
    [Table("Sintomas",Schema ="CAT")]
    public class Sintoma : CrudEntities
    {
        [Key]
        public long IdSintoma { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<FichaDetalle> FichaDetalles { get; set; }
        public virtual ICollection<FichaHemoparasitosis> FichaHemoparasitosis { get; set; }
    }
}
