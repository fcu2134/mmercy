using MercDevs_ej2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
 
//Para cerrar Sesi�n
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

//FIN cerrar sesi�n

namespace MercDevs_ej2.Controllers
{
    [Authorize] //Para que no autorice ingresar a ningun controlador sin estar Autenticado
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MercydevsEjercicio2Context _context;
        public HomeController(ILogger<HomeController> logger , MercydevsEjercicio2Context context )
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            var Recepcionequipo = _context.Recepcionequipos
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdServicioNavigation)
                .Where(r => r.Estado == 1)
                .ToList();

            return View(Recepcionequipo); // Pasar la lista de equipos en proceso a la vista
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
       
        //Para Salir sesi�n
        public async Task<IActionResult>  LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Ingresar", "Login");
        }
        //Fin Salir Sesi�n
    }
}
