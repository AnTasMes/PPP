using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegoProdavnica.Controllers
{
    public class ProizvodController : Controller
    {
        private LegoProdavnicaContext LegoProdavnica = new LegoProdavnicaContext();
        public IActionResult IndexProduct()
        {
            return View(LegoProdavnica.Proizvods.ToList());
        }

        public IActionResult DetailedView()
        {
            return View();
        }

        public IActionResult ViewTest()
        {
            return View(LegoProdavnica.Proizvods.ToList());
        }

    }
}
