namespace LegoProdavnica.Models;

public partial class Proizvod {
    public int ProizvodId { get; set; }

    public string? AlteranteId { get; set; }

    public string Naziv { get; set; } = null!;

    public string Godine { get; set; } = null!;

    public string? Pakovanje { get; set; }

    public string? Tip { get; set; }

    public decimal? Cena { get; set; }

    public bool? NaStanju { get; set; }

    public string? Slika { get; set; }

    public virtual ICollection<Narudzbina> Narudzbinas { get; } = new List<Narudzbina>();

    public virtual ICollection<RacunProizvod> RacunProizvods { get; } = new List<RacunProizvod>();

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; } = new List<Rezervacija>();
}
