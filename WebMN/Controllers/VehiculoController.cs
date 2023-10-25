using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMN.Models;

namespace WebMN.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44392"; 

        public VehiculoController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ActionResult> ConsultarVehiculos()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/vehiculos");
            if (response.IsSuccessStatusCode)
            {
                var vehiculos = await response.Content.ReadAsAsync<IEnumerable<VehiculoModel>>();
                return View(vehiculos);
            }
            else
            {
                // Manejar errores
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult RegistrarVehiculo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarVehiculo(VehiculoModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/vehiculos", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ConsultarVehiculos");
            }
            else
            {
                // Manejar errores
                return View("Error");
            }
        }
    }
}
