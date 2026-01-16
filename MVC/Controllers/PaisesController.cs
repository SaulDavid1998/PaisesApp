using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Controllers
{
   
    public class PaisesController : Controller
    {
        private HttpClient _httpClient;
        public PaisesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        public async Task< ActionResult> Detalles(string id = "argentina")
        {
            
            var url = "https://localhost:7009/api/pais/nombre/" + id;


            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                if(((int)response.StatusCode)==404)
                { 
                    return RedirectToAction("NotFound","Error");
                }

                if (((int)response.StatusCode) == 400)
                {
                    return RedirectToAction("BadRequest","Error");
                }

                if (((int)response.StatusCode) == 409)
                {
                    return RedirectToAction("Conflict","Error");
                }

            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<Pais>(json);

            

            return View(data);
        }

        public async Task<ActionResult> Region(string id="asia",int pagina=1)
        {

            var url = "https://localhost:7009/api/pais/region/" + id;
            
            var response = await _httpClient.GetAsync(url);

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

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<Pais>>(json).OrderBy(l => l.nombre).ToList();



            var urlContinentes = "https://localhost:7009/api/pais/continentes";

            var respuesta = await _httpClient.GetAsync(urlContinentes);

            var jsonContinentes = await respuesta.Content.ReadAsStringAsync();

            var continentes = JsonSerializer.Deserialize<List<string>>(jsonContinentes);


            ViewBag.Continentes = continentes;
            ViewBag.Region = id;

            int tamañoPagina = 15;

            var totalRegistros = data.Count;

            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);

            data = data.Skip((pagina - 1) * tamañoPagina).Take(tamañoPagina).ToList();

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPais(string nombre,string paisActual)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                TempData["Error"] = "La barra de busqueda no puede estar vacia";
                return RedirectToAction("Detalles", new { id = paisActual });
            }

            var url = "https://localhost:7009/api/pais/nombre/" + nombre.Trim();

            var response = await _httpClient.GetAsync(url);

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

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<Pais>(json);



            return RedirectToAction("Detalles", new {id=nombre});
        }

    }
}
