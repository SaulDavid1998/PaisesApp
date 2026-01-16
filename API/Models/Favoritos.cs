using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Favoritos
    {
        [Key]
        public string codigo { get; set; }

        public string pais { get; set; }

        public string json { get; set; }
    }
}
