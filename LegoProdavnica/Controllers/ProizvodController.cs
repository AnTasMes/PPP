using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LegoProdavnica.Controllers {
    public class ProizvodController : Controller {
        private LegoProdavnicaContext _context = new LegoProdavnicaContext();

        [HttpPost]
        public IActionResult IndexProduct(List<Proizvod> proizvodi) {
            System.Diagnostics.Debug.WriteLine("IndexProduct run");
            return View(proizvodi);
        }

        public IActionResult GetAllProducts() {
            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult DetailedView(Proizvod item) {
            var recs = _context.Recenzijas.Where(r => r.ProizvodId == item.ProizvodId).ToList();

            foreach (var r in recs) {
                r.Korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == r.KorisnikId);
            }

            item.Recenzijas = recs;

            return View(item);
        }

        public IActionResult ViewTest() {
            return View(_context.Proizvods.ToList());
        }

        public IActionResult buyCart() {
            List<Proizvod> items = new List<Proizvod>();
            string? itemsCookie = "";
            Request.Cookies.TryGetValue("items", out itemsCookie);

            if (itemsCookie != null) {
                items = JsonConvert.DeserializeObject<List<Proizvod>>(itemsCookie);
            }

            if (items.Count > 0) {
                Racun racun = new Racun();
                decimal? ukupnaCena = 0;
                List<RacunProizvod> rps = new List<RacunProizvod>();

                foreach (var i in items) {
                    ukupnaCena += i.Cena;
                }

                racun.UkupnaCena = ukupnaCena;

                try {
                    _context.Racuns.Add(racun);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                }


                foreach (var i in items) {
                    RacunProizvod rp = new RacunProizvod();
                    rp.RacunId = racun.RacunId;
                    rp.ProizvodId = i.ProizvodId;
                    rp.DatumDodavanja = DateTime.Now;

                    rps.Add(rp);
                }


                try {
                    foreach (var item in rps) {
                        _context.RacunProizvods.Add(item);
                    }

                    _context.SaveChanges();
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            Response.Cookies.Delete("items");

            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult addToCart(Proizvod item) {
            List<Proizvod> items = new List<Proizvod>();
            string? itemsCookie = "";
            Request.Cookies.TryGetValue("items", out itemsCookie);

            if (itemsCookie != null) {
                items = JsonConvert.DeserializeObject<List<Proizvod>>(itemsCookie);
            }

            items.Add(item);
            string serialized = JsonConvert.SerializeObject(items);
            Response.Cookies.Delete("items");
            Response.Cookies.Append("items", serialized);

            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        [HttpPost]
        public IActionResult Filter(string order, string name) {
            System.Diagnostics.Debug.WriteLine("Order: " + order + " | Name: " + name);

            if (order.Equals("descending")) {
                var proizvodi = from p in _context.Proizvods
                                where String.IsNullOrEmpty(name) ? p.Naziv.Contains(String.Empty) : p.Naziv.Contains(name)
                                orderby p.Cena descending
                                select p;

                return View("IndexProduct", proizvodi);
            } else {
                var proizvodi = from p in _context.Proizvods
                                where String.IsNullOrEmpty(name) ? p.Naziv.Contains(String.Empty) : p.Naziv.Contains(name)
                                orderby p.Cena ascending
                                select p;

                return View("IndexProduct", proizvodi);
            }
        }

    }
}
