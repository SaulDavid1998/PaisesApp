using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class PaisesContext : DbContext
    {
        public PaisesContext(DbContextOptions<PaisesContext> options) : base(options)
        {
        }

       public DbSet<Favoritos> Favoritos { get; set; }
    }
}
