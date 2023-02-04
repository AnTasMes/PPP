using LegoProdavnica.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LegoProdavnica.Controllers {
    public class ProizvodController : Controller {
        private LegoProdavnicaContext _context = new LegoProdavnicaContext();
        private readonly IEmailSender _emailSender;

        public ProizvodController(IEmailSender sender) {
            _emailSender = sender;
        }

        [HttpPost]
        public IActionResult IndexProduct(List<Proizvod> proizvodi) {
            System.Diagnostics.Debug.WriteLine("IndexProduct run");
            return View(proizvodi);
        }

        public IActionResult GetAllProducts() {
            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult ViewTest() {
            return View(_context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult List() {
            return View(_context.Proizvods.ToList<Proizvod>());
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


        public IActionResult DetailedView(Proizvod item) {
            var recs = _context.Recenzijas.Where(r => r.ProizvodId == item.ProizvodId).ToList();

            foreach (var r in recs) {
                r.Korisnik = _context.Profils.FirstOrDefault(p => p.ProfilId == r.KorisnikId);
            }

            item.Recenzijas = recs;

            return View(item);
        }

        public IActionResult buyCart(Profil user) {
            List<Proizvod> items = new List<Proizvod>();
            string? itemsCookie = "";
            Request.Cookies.TryGetValue("items", out itemsCookie);

            if (itemsCookie != null) {
                items = JsonConvert.DeserializeObject<List<Proizvod>>(itemsCookie);
            }

            System.Diagnostics.Debug.WriteLine(items.Count);

            if (items.Count > 0) {
                Racun racun = new Racun();
                decimal? ukupnaCena = 0;
                List<RacunProizvod> rps = new List<RacunProizvod>();

                foreach (var i in items) {
                    ukupnaCena += i.Cena;
                }

                racun.UkupnaCena = ukupnaCena;
                racun.DatumIzdavanja = DateTime.Now;
                racun.KorisnikId = user.ProfilId;

                try {
                    _context.Racuns.Add(racun);
                    _context.SaveChanges();

                    foreach (var i in items) {
                        var rp = new RacunProizvod();
                        rp.DatumDodavanja = DateTime.Now;
                        rp.RacunId = racun.RacunId;
                        rp.ProizvodId = i.ProizvodId;
                        rp.Proizvod = _context.Proizvods.FirstOrDefault(p => p.ProizvodId == i.ProizvodId);
                        rps.Add(rp);

                        _context.RacunProizvods.Add(rp);
                        _context.SaveChanges();
                    }

                    racun.RacunProizvods = rps;
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine("Greska kod slanja poruke");

                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                try {
                    System.Diagnostics.Debug.WriteLine("Slanje poruke");

                    var message = new Message(user.Email, "Lego prodavnica - Racun - " + DateTime.Now, "");

                    foreach (var p in rps) {
                        message.Body += "Naziv: " + p.Proizvod.Naziv + " - Cena: " + p.Proizvod.Cena + "\n";
                    }

                    message.Body += "Ukupna vrednost je: " + racun.UkupnaCena + " RSD";

                    _emailSender.SendEmail(message);
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine("Greska kod slanja poruke");
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

        public IActionResult oceni(int user, int item, int ocena) {
            Recenzija rec = new Recenzija();
            rec.Ocena = ocena;
            rec.ProizvodId = item;
            rec.DatumKreacija = DateTime.Now;
            rec.KorisnikId = user;
            rec.Korisnik = _context.Profils.FirstOrDefault(k => k.ProfilId == user);

            try {
                _context.Recenzijas.Add(rec);
                _context.SaveChanges();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return RedirectToAction("DetailedView", _context.Proizvods.FirstOrDefault(p => p.ProizvodId == item));
        }

        public IActionResult naruci(string adresa, int user, int item) {
            Narudzbina n = new Narudzbina();
            n.Adresa = adresa;
            n.KorisnikId = user;
            n.Korisnik = _context.Profils.FirstOrDefault(k => k.ProfilId == user);
            n.ProizvodId = item;
            n.DatumKreacije = DateTime.Now;
            n.DatumDostave = DateTime.Now.AddDays(5);

            try {
                _context.Narudzbinas.Add(n);
                _context.SaveChanges();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }

        public IActionResult rezervisi(DateTime datum, int user, int item) {
            Rezervacija r = new Rezervacija();
            r.DatumKreacije = DateTime.Now;
            r.DatumDostave = datum;
            r.KorisnikId = user;
            r.Korisnik = _context.Profils.FirstOrDefault(k => k.ProfilId == user);
            r.ProizvodId = item;

            try {
                _context.Rezervacijas.Add(r);
                _context.SaveChanges();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View("IndexProduct", _context.Proizvods.ToList<Proizvod>());
        }
    }
}