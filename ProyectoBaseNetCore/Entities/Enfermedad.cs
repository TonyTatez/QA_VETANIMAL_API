using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VET_ANIMAL_API.Entities;

namespace ProyectoBaseNetCore.Entities
{
    [Table("Enfermedad",Schema ="CAT")]
    public class Enfermedad : CrudEntities
    {
        [Key]
        public long IdEnfermedad { get; set; }
        public string CodigoEnfermedad { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Resultado> Resultados { get; set; }
        public virtual ICollection<FichaHemoparasitosis> FichaHemoparasitosis { get; set; }
    }
}
