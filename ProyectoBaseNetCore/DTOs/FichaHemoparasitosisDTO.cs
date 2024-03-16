namespace VET_ANIMAL_API.DTOs
{
    public class FichaHemoparasitosisDTO 
    {
        public long IdFichaHemo { get; set; }
        public string CodigoFichaHemo { get; set; }      
        public long IdHistoriaClinica { get; set; }
        public long IdFichaControl { get; set; }
        public DateTime Fecha { get; set; }
        public long IdMascota { get; set; }
        public long IdEnfermedad { get; set; }
        public string Enfermedad { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public string NombreMascota { get; set; }     
        public string Cliente { get; set; }
    }

    public class FichaHemoparasitosisTEST
    {
        public long IdFichaHemo { get; set; }
        public string CodigoFichaHemo { get; set; }
        public long IdHistoriaClinica { get; set; }
        public long IdFichaControl { get; set; }
        public DateTime Fecha { get; set; }
        public long IdMascota { get; set; }
        public long IdEnfermedad { get; set; }
        public long? IdContenido { get; set; }
        public string Enfermedad { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public string NombreMascota { get; set; }
        public string Cliente { get; set; }
        public float? Peso { get; set; }
        public string Raza { get; set; }

    }
}
