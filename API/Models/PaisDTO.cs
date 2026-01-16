namespace API.Models
{
    public class PaisDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public string Capital { get; set; }

        public int Poblacion { get; set; }

        public string BanderaUrl { get; set; }

        public string MapaUrl { get; set; }

        public string EscudoUrl { get; set; }

        public string Moneda { get; set; }

        public string SimboloMoneda { get; set; }

        public List<string> Idiomas { get; set; }

        public string Continente { get; set; }


    }
}
