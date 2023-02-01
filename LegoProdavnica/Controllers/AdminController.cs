using Microsoft.AspNetCore.Mvc;

namespace LegoProdavnica.Controllers {
    public class AdminController : Controller {
        public IActionResult IndexAdmin() {
            return View();
        }

        public IActionResult ListEmployees() {
            return View();
        }

        public IActionResult ListOrders() {
            return View();
        }
    }
}
