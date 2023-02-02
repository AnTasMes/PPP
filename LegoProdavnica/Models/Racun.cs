using System;
using System.Collections.Generic;

namespace LegoProdavnica.Models;

public partial class Racun
{
    public int RacunId { get; set; }

    public string? AlternateId { get; set; }

    public decimal? UkupnaCena { get; set; }

    public DateTime? DatumIzdavanja { get; set; }

    public int? RadnikId { get; set; }

    public int? KorisnikId { get; set; }

    public virtual Profil? Korisnik { get; set; }

    public virtual ICollection<RacunProizvod> RacunProizvods { get; } = new List<RacunProizvod>();

    public virtual Profil? Radnik { get; set; }
}
