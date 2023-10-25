using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMN.Models;

namespace WebMN.Controllers
{
    public class VendedorController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44392"; 

        public VendedorController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ActionResult> ConsultarVendedores()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/vendedores");
            if (response.IsSuccessStatusCode)
            {
                var vendedores = await response.Content.ReadAsAsync<IEnumerable<VendedorModel>>();
                return View(vendedores);
            }
            else
            {
                // Manejar errores
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult RegistrarVendedor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarVendedor(VendedorModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/vendedores", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ConsultarVendedores");
            }
            else
            {
                // Manejar errores
                return View("Error");
            }
        }
    }
}
