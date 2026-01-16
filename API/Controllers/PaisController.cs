using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private HttpClient _httpClient;
        public PaisController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        [HttpGet("nombre/{Nombre}")]
        public async Task<ActionResult> GetPais(string Nombre)
        {
            var url = "https://restcountries.com/v3.1/translation/" + Nombre;

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                return StatusCode((int)response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<ApiResponsePais>>(json);


            
            //if (data == null)
            //{
            //    return NotFound();
            //}

            var registro = data.Where(json => json.translations["spa"].common.ToLower() == Nombre.ToLower()).FirstOrDefault();

            if(registro == null)
            {
                return NotFound();
            }
            var pais = new PaisDTO
            {
                Codigo= registro.ccn3,
                Nombre = registro.translations["spa"].common,
                Capital = registro.capital?.FirstOrDefault()??"Sin capital",
                Poblacion = registro.population,
                BanderaUrl = registro.flags.png,
                EscudoUrl = registro.coatOfArms.png,
                MapaUrl = registro.maps.googleMaps,
                Moneda = registro.currencies.Values.FirstOrDefault().name,
                SimboloMoneda = registro.currencies.Values.FirstOrDefault().symbol,
                Idiomas = registro.languages.Values.ToList(),
                Continente=registro.region
            };

            return Ok(pais);

        }

        [HttpGet("region/{region:alpha}")]
        public async Task<ActionResult> GetRegion(string region)
        {

            var url = "https://restcountries.com/v3.1/region/" + region;

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<ApiResponsePais>>(json);

            if (data == null)
            {
                return NotFound();
            }

            List<PaisDTO> lstPaises = new List<PaisDTO>();


            foreach (var item in data)
            {              

                var moneda = item.currencies?.FirstOrDefault().Value;

                var paisDTO = new PaisDTO
                {
                    Codigo= item.ccn3,
                    Nombre = item.translations["spa"].common,
                    Capital = item.capital?.FirstOrDefault()??"Sin capital",
                    Poblacion = item.population,
                    BanderaUrl = item.flags.png,
                    Moneda = moneda.name,
                    SimboloMoneda = moneda.symbol,
                    EscudoUrl = item.coatOfArms.png,
                    MapaUrl = item.maps.googleMaps,
                    Idiomas = item.languages.Values.ToList(),
                    Continente = item.region
                };
                lstPaises.Add(paisDTO);
            }

            return Ok(lstPaises);
        }


        [HttpGet("continentes")]
        public async Task<ActionResult> GetContinentes()
        {
            var url = "https://restcountries.com/v3.1/translation/all";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();

            }
                

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<ApiResponsePais>>(json);

            if (data == null)
            {
                return NotFound();
            }
                
            List<string>lstContinentes = data.Select(p => p.region).Distinct().ToList();


            //List<PaisDTO> lstPaises = new List<PaisDTO>();


            //foreach (var item in data)
            //{

            //    var moneda = item.currencies?.FirstOrDefault().Value;

            //    var paisDTO = new PaisDTO
            //    {
            //        Codigo = item.ccn3,
            //        Nombre = item.translations["spa"].common,
            //        Capital = item.capital?.FirstOrDefault() ?? "Sin capital",
            //        Poblacion = item.population,
            //        BanderaUrl = item.flags.png,
            //        Moneda = moneda.name,
            //        SimboloMoneda = moneda.symbol,
            //        EscudoUrl = item.coatOfArms.png,
            //        MapaUrl = item.maps.googleMaps,
            //        Idiomas = item.languages.Values.ToList(),
            //        Continente = item.region
            //    };
            //    lstPaises.Add(paisDTO);
            //}

            return Ok(lstContinentes);

        }


    }
}
