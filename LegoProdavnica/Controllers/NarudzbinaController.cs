using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers {
    public class NarudzbinaController : Controller {

        LegoProdavnicaContext _context = new LegoProdavnicaContext();

        public IActionResult Index() {
            return View();
        }

        public IActionResult List() {
            var nars = _context.Narudzbinas.ToList<Narudzbina>();

            foreach (var item in nars) {
                item.Proizvod = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == item.ProizvodId);
                item.Korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == item.KorisnikId);

                if (item.Korisnik == null || item.Proizvod == null) {
                    nars.Remove(item);
                }
            }

            return View(nars);
        }

        public IActionResult Cancel(int id) {
            var narudzbina = _context.Narudzbinas.FirstOrDefault<Narudzbina>(n => n.NarudzbinaId == id);

            if (narudzbina == null) {
                return RedirectToAction("List");
            }

            narudzbina.Proizvod = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == narudzbina.ProizvodId);
            narudzbina.Korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == narudzbina.KorisnikId);

            if (narudzbina.Korisnik == null || narudzbina.Proizvod == null) {
                return RedirectToAction("List");
            }

            return View(narudzbina);
        }

        [HttpPost]
        public IActionResult Cancel(Narudzbina model) {
            if (ModelState.IsValid) {
                try {
                    _context.Narudzbinas.Remove(model);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("List");
        }
    }
}
