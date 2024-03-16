using ProyectoBaseNetCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VET_ANIMAL_API.Entities;

namespace ProyectoBaseNetCore.Entities
{
    [Table("TablaContenido", Schema = "DET")]
    public class TablaContenido : CrudEntities
    {
        [Key]
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string Resultado { get; set; }

        [ForeignKey("FichaHemoparasitosis")]
        public long IdFichaHemo { get; set; }
        public virtual FichaHemoparasitosis FichaHemoparasitosis { get; set; }
    }
}
