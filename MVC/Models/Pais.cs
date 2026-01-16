namespace MVC.Models
{
    public class Pais
    {
        public string codigo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;

        public string capital { get; set; } = string.Empty;

        public int poblacion { get; set; }

        public string banderaUrl { get; set; } = string.Empty;

        public string mapaUrl { get; set; } = string.Empty;

        public string escudoUrl { get; set; } = string.Empty;

        public string moneda { get; set; } = string.Empty;

        public string simboloMoneda { get; set; } = string.Empty;

        public List<string> idiomas { get; set; } = [];

        public string continente { get; set; } = string.Empty;
    }
}
