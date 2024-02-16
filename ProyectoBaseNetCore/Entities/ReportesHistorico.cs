using ProyectoBaseNetCore.Entities;
using System.ComponentModel.DataAnnotations;
namespace VET_ANIMAL_API.Entities
{
    public class ReportesHistorico : CrudEntities
    {

        [Key]
        public long IdReporte { get; set; }

    }
}
