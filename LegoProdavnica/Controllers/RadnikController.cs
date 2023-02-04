using Microsoft.AspNetCore.Mvc;
using LegoProdavnica.Models;


namespace LegoProdavnica.Controllers
{
    public class RadnikController : Controller
    {
        LegoProdavnicaContext _context = new LegoProdavnicaContext();

        private readonly IEmailSender _emailSender;

        public RadnikController(IEmailSender sender)
        {
            this._emailSender = sender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListKorisnika()
        {
            var uloga = _context.Ulogas.FirstOrDefault(u => u.Tip == "Korisnik");

            if(uloga == null)
            {
                return NotFound();
            }

            return View(_context.Profils.Where(p => p.UlogaId == uloga.UlogaId).ToList<Profil>());
        }

        public IActionResult ListProizvoda()
        {
            return View(_context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult Message(int id)
        {
            var korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == id);

            if(korisnik == null)
            {
                return RedirectToAction("ListKorisnika");
            }

            return View(korisnik);
        }

        [HttpPost]
        public IActionResult Send(Profil model, string Subject, string Content)
        {
            System.Diagnostics.Debug.WriteLine("Sending message to user ...");
            System.Diagnostics.Debug.WriteLine($"Subject: {Subject} | Content: {Content}");

            if (String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.KorisnickoIme))
            {
                return RedirectToAction("Message", model.ProfilId);
            }

            _emailSender.SendEmail(new Message(model.Email, Subject, Content));

            return RedirectToAction("ListKorisnika");
        }
    }
}
