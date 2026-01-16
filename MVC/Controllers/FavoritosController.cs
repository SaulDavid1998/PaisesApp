using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Text;
using System.Text.Json;

namespace MVC.Controllers
{
    public class FavoritosController : Controller
    {
        public HttpClient HttpClient;

        public FavoritosController(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public ActionResult ListaFavoritos()
        {

            var favoritos= HttpContext.Session.GetObjectFromJson<List<Pais>>("favoritos")??new List<Pais>();

            return View(favoritos);
        }


        [HttpPost]
        public ActionResult AgregarFavorito(Pais objPais)
        {
            var lstFavoritos = HttpContext.Session.GetObjectFromJson<List<Pais>>("favoritos")??new List<Pais>();
            


            lstFavoritos.Add(objPais);

            HttpContext.Session.SetObject("favoritos",lstFavoritos);
            HttpContext.Session.SetInt32("contador_Favoritos", lstFavoritos.Count);
            TempData["Titulo"] = objPais.nombre + " agregado a favoritos";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarFavorito(Pais objPais)
        {
            var lstFavoritos = HttpContext.Session.GetObjectFromJson<List<Pais>>("favoritos");
            if (lstFavoritos != null)
            {
                var paisAEliminar = lstFavoritos.Where(p => p.nombre == objPais.nombre).FirstOrDefault();
                if (paisAEliminar != null)
                {
                    lstFavoritos.Remove(paisAEliminar);
                    HttpContext.Session.SetObject("favoritos", lstFavoritos);
                    HttpContext.Session.SetInt32("contador_Favoritos", lstFavoritos.Count);
                }
            }
            TempData["Eliminado"] = objPais.nombre + " eliminado de favoritos";

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<ActionResult> GuardarFavoritos()
        {
            var lstFavoritos = HttpContext.Session.GetObjectFromJson<List<Pais>>("favoritos");


            var lstAPI = lstFavoritos.Select(r => new
            {
                codigo = r.codigo,
                pais = r.nombre,
                json = JsonSerializer.Serialize(r)
            }).ToList();


            var contenido = new StringContent(JsonSerializer.Serialize(lstAPI), Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync("https://localhost:7009/api/favoritos", contenido);

            if (response.IsSuccessStatusCode == false)
            {
                if (((int)response.StatusCode) == 404)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                if (((int)response.StatusCode) == 400)
                {
                    return RedirectToAction("BadRequest", "Error");
                }

                if (((int)response.StatusCode) == 409)
                {
                    return RedirectToAction("Conflict", "Error");
                }

            }
            HttpContext.Session.Remove("favoritos");
            HttpContext.Session.SetInt32("contador_Favoritos", 0);
            TempData["Guardado"] = "Favoritos guardados en la base de datos";
            return RedirectToAction("Index", "Home");
        }

    }
}
