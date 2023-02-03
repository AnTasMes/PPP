using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;


namespace LegoProdavnica.Controllers {
    public class AdminController : Controller {

        private LegoProdavnicaContext _context = new LegoProdavnicaContext();

        public IActionResult IndexAdmin() {
            System.Diagnostics.Debug.WriteLine("Index admin");

            return View();
        }

        public IActionResult ListEmployees() {
            System.Diagnostics.Debug.WriteLine("Listing employees...");

            var role = _context.Ulogas.FirstOrDefault(u => u.Tip.Equals("Radnik"));

            if (role == null) {
                return RedirectToAction("IndexAdmin");
            }

            int radnikRoleId = role.UlogaId;

            System.Diagnostics.Debug.WriteLine("RoleID: " + radnikRoleId);

            return View(_context.Profils.Where(p => p.UlogaId == radnikRoleId).ToList());
        }

        public IActionResult ListOrders() {
            return View();
        }

        public IActionResult Edit(int id) {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Edit: " + id);

            return View(_context.Profils.FirstOrDefault(p => p.ProfilId == id));
        }

        public IActionResult Create() {
            return View();
        }

        public IActionResult Delete(int id) {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Delete: " + id);

            return View(_context.Profils.FirstOrDefault(p => p.ProfilId == id));
        }

        [HttpPost]
        public IActionResult Edit(Profil model) {
            _context.Profils.Update(model);
            _context.SaveChanges();

            return RedirectToAction("ListEmployees");
        }

        [HttpPost]
        public IActionResult Delete(Profil model) {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Delete: " + model.ProfilId);

            var profil = _context.Profils.FirstOrDefault(p => p.ProfilId == model.ProfilId);

            if (profil == null) {
                return RedirectToAction("ListEmployees");
            }

            _context.Profils.Remove(profil);
            _context.SaveChanges();

            return RedirectToAction("ListEmployees");
        }

        [HttpPost]
        public IActionResult Create(Profil model) {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Create: " + model.ProfilId);

            var role = _context.Ulogas.FirstOrDefault(u => u.Tip == "Radnik");

            if (role == null) {
                return RedirectToAction("ListEmployees");
            }

            int roleId = role.UlogaId;

            model.UlogaId = roleId;

            _context.Profils.Add(model);
            _context.SaveChanges();


            return RedirectToAction("ListEmployees");
        }
    }
}
