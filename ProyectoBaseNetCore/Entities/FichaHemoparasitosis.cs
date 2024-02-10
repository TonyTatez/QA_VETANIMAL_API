using ProyectoBaseNetCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VET_ANIMAL_API.Entities
{
    [Table("FichaHemoparasitosis", Schema = "DET")]
    public class FichaHemoparasitosis : CrudEntities
    {
        [Key]
        public long IdFichaHemo { get; set; }
        public string CodigoFichaHemo { get; set; }
        [ForeignKey("HistoriaClinica")]
        public long IdHistoriaClinica { get; set; }
        [ForeignKey("Sintomas")]
        public long? IdSintoma { get; set; }

        [ForeignKey("Enfermedad")]
        public long IdEnfermedad{ get; set; }
        public string Observacion { get; set; }
        public string Tratamiento { get; set; }

        public long IdFichaControl { get; set; } 
        public virtual HistoriaClinica HistoriaClinica { get; set; }
        public virtual Enfermedad Enfermedad { get; set; }
        public virtual Sintoma Sintomas { get; set; }

        public virtual FichaControl FichaControl { get; set; } 
    }
}
