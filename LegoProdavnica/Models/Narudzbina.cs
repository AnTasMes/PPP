using System;
using System.Collections.Generic;

namespace LegoProdavnica.Models;

public partial class Narudzbina
{
    public int NarudzbinaId { get; set; }

    public DateTime? DatumKreacije { get; set; }

    public DateTime? DatumDostave { get; set; }

    public DateTime? Adresa { get; set; }

    public int? KorisnikId { get; set; }

    public int? ProizvodId { get; set; }

    public virtual Profil? Korisnik { get; set; }

    public virtual Proizvod? Proizvod { get; set; }
}
