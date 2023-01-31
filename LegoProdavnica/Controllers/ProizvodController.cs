using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers
{
    public class ProizvodController : Controller
    {
        public IActionResult IndexProduct()
        {
            return View();
        }

        public IActionResult DetailedView()
        {
            return View();
        }
    }
}
