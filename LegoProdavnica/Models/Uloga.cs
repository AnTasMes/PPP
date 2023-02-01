namespace LegoProdavnica.Models;

public partial class Uloga {
    public int UlogaId { get; set; }

    public string? Opis { get; set; }

    public string? Tip { get; set; }

    public virtual ICollection<Profil> Profils { get; } = new List<Profil>();
}
