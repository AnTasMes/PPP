using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers
{
    public class ProizvodController : Controller
    {
        private LegoProdavnicaContext _context = new LegoProdavnicaContext();

        [HttpPost]
        public IActionResult IndexProduct(List<Proizvod> proizvodi)
        {
            System.Diagnostics.Debug.WriteLine("IndexProduct run");
            return View(proizvodi);
        }

        public IActionResult GetAllProducts()
        {
            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult DetailedView()
        {
            return View();
        }

        public IActionResult ViewTest()
        {
            return View(_context.Proizvods.ToList());
        }

		public IActionResult NarudzbineVIew() {
			return View(_context.Narudzbinas.ToList());
		}

        public IActionResult DeleteNarudzbina(int id) {
            _context.Narudzbinas.Remove(_context.Narudzbinas.FirstOrDefault(n => n.NarudzbinaId == id));
            _context.SaveChanges();
            return View();  
        }

		[HttpPost]
        public IActionResult Filter(string order, string name)
        {
            System.Diagnostics.Debug.WriteLine("Order: " + order + " | Name: "+ name);

            if (order.Equals("descending"))
            {
                var proizvodi = from p in _context.Proizvods
                                where String.IsNullOrEmpty(name) ? p.Naziv.Contains(String.Empty) : p.Naziv.Contains(name)
                                orderby p.Cena descending
                                select p;

                return View("IndexProduct", proizvodi);
            }
            else
            {
                var proizvodi = from p in _context.Proizvods
                                where String.IsNullOrEmpty(name) ? p.Naziv.Contains(String.Empty) : p.Naziv.Contains(name)
                                orderby p.Cena ascending
                                select p;

                return View("IndexProduct", proizvodi);
            }
        }

    }
}
