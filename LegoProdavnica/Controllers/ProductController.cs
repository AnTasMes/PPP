using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult IndexProduct()
        {
            return View();
        }
    }
}
