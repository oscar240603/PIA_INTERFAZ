using API_INTERFAZ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using API_INTERFAZ.Servicios;

namespace API_INTERFAZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;

        public HomeController(IServicio_API servicio_Api)
        {
            _servicioApi = servicio_Api;
        }

        public async Task<IActionResult> Index()
        {
            List<Alumno> lista = await _servicioApi.Lista();




            return View(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
