using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers {
    public class RadnikController : Controller {
        LegoProdavnicaContext _context = new LegoProdavnicaContext();

        private readonly IEmailSender _emailSender;

        public RadnikController(IEmailSender sender) {
            this._emailSender = sender;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult ListKorisnika() {
            var uloga = _context.Ulogas.FirstOrDefault(u => u.Tip == "Korisnik");

            if (uloga == null) {
                return NotFound();
            }

            return View(_context.Profils.Where(p => p.UlogaId == uloga.UlogaId).ToList<Profil>());
        }

        public IActionResult ListProizvoda() {
            return View(_context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult Message(int id) {
            var korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == id);

            if (korisnik == null) {
                return RedirectToAction("ListKorisnika");
            }

            return View(korisnik);
        }

        [HttpPost]
        public IActionResult Send(Profil model, string Subject, string Content) {
            System.Diagnostics.Debug.WriteLine("Sending message to user ...");
            System.Diagnostics.Debug.WriteLine($"Subject: {Subject} | Content: {Content}");

            if (String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.KorisnickoIme)) {
                return RedirectToAction("Message", model.ProfilId);
            }

            _emailSender.SendEmail(new Message(model.Email, Subject, Content));

            return RedirectToAction("ListKorisnika");
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Proizvod model) {
            if (ModelState.IsValid) {
                try {
                    _context.Proizvods.Add(model);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("ListProizvoda");
        }

        [HttpPost]
        public IActionResult Delete(Proizvod model) {
            try {
                var yes = _context.RacunProizvods.Where(rp => rp.ProizvodId == model.ProizvodId);
                _context.RacunProizvods.RemoveRange(yes);
                _context.SaveChanges();

                var yes2 = _context.Recenzijas.Where(r => r.ProizvodId == model.ProizvodId);
                _context.Recenzijas.RemoveRange(yes2);
                _context.SaveChanges();

                _context.Proizvods.Remove(_context.Proizvods.FirstOrDefault(p => p.ProizvodId == model.ProizvodId));
                _context.SaveChanges();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("ListProizvoda");
        }

        public IActionResult Delete(int id) {
            var proizvod = _context.Proizvods.FirstOrDefault<Proizvod>(p => p.ProizvodId == id);

            if (proizvod == null) {
                return RedirectToAction("ListProizvoda");
            }

            return View(proizvod);
        }

        public IActionResult Edit(int id) {
            var proizvod = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == id);

            if (proizvod == null) {
                return RedirectToAction("ListProizvoda");
            }

            return View(proizvod);
        }

        [HttpPost]
        public IActionResult Edit(Proizvod model) {
            if (model == null) {
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Proizvods.Update(model);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return View();
                }
            }

            return RedirectToAction("ListProizvoda");
        }
    }
}
