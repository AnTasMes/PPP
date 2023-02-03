using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LegoProdavnica.Controllers {
    public class ProfilController : Controller {

        private LegoProdavnicaContext _context = new LegoProdavnicaContext();

        public ActionResult Register() {
            return View();
        }

        public ActionResult Login() {
			return View();
        }

        // GET: UserController
        public ActionResult Index() {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(Profil model, string confirm) {
            System.Diagnostics.Debug.WriteLine(model.KorisnickoIme + " | Password:  " + model.Sifra + " | Confirmed: " + confirm);
            if (confirm != model.Sifra) {
                return View();
            }

            if (ModelState.IsValid) {
                try {
                    System.Diagnostics.Debug.WriteLine("UlogaID: " + _context.Ulogas.First(u => u.Tip.Equals("Korisnik")).UlogaId);

                    model.UlogaId = _context.Ulogas.First(u => u.Tip.Equals("Korisnik")).UlogaId;
                    _context.Profils.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                } catch (Exception ex) {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Profil model) {
            System.Diagnostics.Debug.WriteLine(model.KorisnickoIme + " | Password:  " + model.Sifra);
            if (ModelState.IsValid) {
                try {
                    if (_context.Profils.Any(p => p.KorisnickoIme.Equals(model.KorisnickoIme) && p.Sifra.Equals(p.Sifra))) {
						var opt = new CookieOptions();
						opt.Expires = DateTime.UtcNow.AddHours(1);

                        model.Uloga = _context.Ulogas.FirstOrDefault(u => u.UlogaId == model.UlogaId);

						string serialized = JsonConvert.SerializeObject(model);
						Response.Cookies.Append("token", serialized, opt);

						System.Diagnostics.Debug.WriteLine("Logged in");
                        return RedirectToAction("Index");
                    }
                } catch (Exception ex) {
                    return View();
                }
            }
            return View();
        }
    }
}
