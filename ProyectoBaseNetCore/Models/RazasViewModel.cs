namespace VET_ANIMAL_API.Models
{
    public class RazasViewModel
    {

        public long idRaza { get; set; }
        public string descripcion { get; set; }
    }

    public class VeterinarioViewModel
    {

        public long id { get; set; }
        public string nombres { get; set; }
        public string identificacion { get; set; }
        public string sede { get; set; }
        public string rol { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }
    public class RazaCantidadViewModel
    {
        public string DescripcionRaza { get; set; }
        public int Cantidad { get; set; }
    }
}
