using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace API.Models
{
    public class ApiResponsePais
    {
        public string ccn3 { get; set; }
        public Dictionary<string,Nombre> translations { get; set; }
        public List<string> capital { get; set; }
        public int population { get; set; } 
        public Bandera flags { get; set; }

        public Escudo coatOfArms { get; set; }

        public Mapa maps { get; set; }

        public Dictionary<string, Moneda> currencies { get; set; }

        public Dictionary<string, string> languages { get; set; }

        public string region { get; set; }
    }
}
