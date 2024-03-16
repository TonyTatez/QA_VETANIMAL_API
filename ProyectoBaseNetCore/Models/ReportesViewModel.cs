using ProyectoBaseNetCore.Entities;

namespace VET_ANIMAL_API.Models
{
    public class ReportesViewModel
    {

       
    }
    public class DiagnosticoViewModel
    {

        public long idEnfermedad { get; set; }
        public string nombre { get; set; }
    }


    public class ReportesHistoricoFiltros
    {
        public long IdFichaControl { get; set; }
        public string CodigoFichaControl { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public long IdHistoriaClinica { get; set; }
        public long IdMotivo { get; set; }
        public long IdMascota { get; set; }
        public string Motivo { get; set; }
        public float? Peso { get; set; }
        public string Observacion { get; set; }
        public string NombreMascota { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public string Cliente { get; set; }
        public string Resultado { get; set; }

    }
    public class ItemTablaContenido
    {
        public long idFichaHemo { get; set; }
        public byte[] content { get; set; }
    }

    public class PDFResultadoModel
    {
        
        public string Resultado { get; set; }
    }
    public class ExcelHistoricoLlantas
    {
        public long Termico { get; set; }
        public string IdVehiculo { get; set; }
        public string KmRecorridos { get; set; }
        public string Posicion { get; set; }
        public string Medida { get; set; }
        public string MarcaNueva { get; set; }
        public string DisenoNuevo { get; set; }
        public string Tipo { get; set; }
        public string MarcaReenc { get; set; }
        public string DisenoReenc { get; set; }
        public string Eje { get; set; }
        public decimal MMOriginal { get; set; }
        public decimal MM1 { get; set; }
        public decimal MM2 { get; set; }
        public decimal MM3 { get; set; }
        public decimal MM4 { get; set; }
        public decimal MMMin { get; set; }
        public decimal MMRetiro { get; set; }
        public decimal PorcentajeDesgaste { get; set; }
        public string FechaInspeccion { get; set; }
        public string Ciudad { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoEstructura { get; set; }
        public string EstadoInspeccion { get; set; }
        public string Observaciones { get; set; }
    }
}
