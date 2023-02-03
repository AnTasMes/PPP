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

        public IActionResult Edit(int id)
        {
            var proizvod = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == id);

            if(proizvod == null)
            {
                return RedirectToAction("List");
            }

            return View(proizvod);
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
            return View(_context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult List()
        {
            return View(_context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var proizvod = _context.Proizvods.FirstOrDefault<Proizvod>(p => p.ProizvodId == id);

            if(proizvod == null)
            {
                return RedirectToAction("List");
            }

            return View(proizvod);
        }

        [HttpPost]
        public IActionResult Create(Proizvod model)
        {
            if (ModelState.IsValid)
            {
                _context.Proizvods.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(Proizvod model)
        {
            _context.Proizvods.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("List");
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

        [HttpPost]
        public IActionResult Edit(Proizvod model)
        {
            if (model == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Proizvods.Update(model);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return View(); //Ispisati da je doslo do greske
                }
            }

            return RedirectToAction("List");
        }


    }
}
