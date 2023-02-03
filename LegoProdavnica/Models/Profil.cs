using System;
using System.Collections.Generic;

namespace LegoProdavnica.Models;

public partial class Profil
{
    public int ProfilId { get; set; }

    public int? UlogaId { get; set; }

    public string? KorisnickoIme { get; set; }

    public string? Sifra { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Narudzbina> Narudzbinas { get; } = new List<Narudzbina>();

    public virtual ICollection<Racun> RacunKorisniks { get; } = new List<Racun>();

    public virtual ICollection<Racun> RacunRadniks { get; } = new List<Racun>();

    public virtual ICollection<Recenzija> Recenzijas { get; } = new List<Recenzija>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; } = new List<Rezervacija>();

    public virtual Uloga? Uloga { get; set; }
}
