using Microsoft.AspNetCore.Mvc;
using LegoProdavnica.Models;


namespace LegoProdavnica.Controllers {
    public class AdminController : Controller {

        private LegoProdavnicaContext _context = new LegoProdavnicaContext();

        public IActionResult IndexAdmin() {
            System.Diagnostics.Debug.WriteLine("Index admin");


            return View();
        }

        public IActionResult ListEmployees() {
            System.Diagnostics.Debug.WriteLine("Listing employees...");

            int radnikRoleId = _context.Ulogas.First(u => u.Tip.Equals("Korisnik")).UlogaId;

            System.Diagnostics.Debug.WriteLine("RoleID: " + radnikRoleId);


            return View(_context.Profils.Where(p => p.UlogaId == radnikRoleId).ToList());
        }

        public IActionResult ListOrders() {
            return View();
        }

        public IActionResult Edit(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Edit: " + id);
            return View(_context.Profils.FirstOrDefault(p => p.ProfilId == id));
        }

        public IActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID Korisnika za Delete: " + id);
            return View(_context.Profils.FirstOrDefault(p => p.ProfilId == id));
        }

        [HttpPost]
        public IActionResult DeleteP(int id)
        {
            _context.Profils.Remove(_context.Profils.FirstOrDefault(p => p.ProfilId == id));
            _context.SaveChanges();

            return RedirectToAction("IndexAdmin");
        }
    }
}
