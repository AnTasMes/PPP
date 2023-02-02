using System;
using System.Collections.Generic;

namespace LegoProdavnica.Models;

public partial class Recenzija
{
    public int RecenzijaId { get; set; }

    public string? AlternateId { get; set; }

    public DateTime? DatumKreacija { get; set; }

    public int? Ocena { get; set; }

    public int? KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public virtual Profil? Korisnik { get; set; }

    public virtual Proizvod? Proizvod { get; set; }
}
