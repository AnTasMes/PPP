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

        public ActionResult Edit(int id) {
            System.Diagnostics.Debug.WriteLine(id);
            return View(_context.Profils.FirstOrDefault(n => n.ProfilId == id));
        }

        [HttpPost]
        public ActionResult Edit(Profil p) {
            if (p == null) {
                return View();
            } else {
                if (ModelState.IsValid) {

                    if (_context.Profils.FirstOrDefault(n => n.KorisnickoIme == p.KorisnickoIme) != null) {
                        TempData["msg"] = "<script>alert('Korisnicko ime vec postoji');</script>";
                        return View();
                    }

                    try {
                        _context.Profils.Update(p);
                        _context.SaveChanges();
                    }catch(Exception ex) {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

                return View("Index");
            }
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

                        Profil korisnik = _context.Profils.FirstOrDefault(p => p.KorisnickoIme == model.KorisnickoIme);

						string serialized = JsonConvert.SerializeObject(korisnik);
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

        public ActionResult Logout() {
            Response.Cookies.Delete("items");
            Response.Cookies.Delete("token");
            return RedirectToAction("Index");
        }
    }
}
