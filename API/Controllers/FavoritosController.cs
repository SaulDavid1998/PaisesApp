using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        public PaisesContext _context;
        public FavoritosController(PaisesContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task <ActionResult> Get()
        {
            var lstFavoritos = await _context.Favoritos.ToListAsync();
            return Ok(lstFavoritos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(List<Favoritos> lstobjFavorito)
        {
            var lstCodigos = await _context.Favoritos
                                    .Select(f => f.codigo)
                                    .ToListAsync();

            foreach (var registro in lstobjFavorito)
            {
                if (lstCodigos.Contains(registro.codigo))
                {
                    return Conflict();
                }

                _context.Favoritos.Add(registro);
            }



            await _context.SaveChangesAsync();
            return Ok();


        }

    }
}
