using System;
using System.Collections.Generic;

namespace LegoProdavnica.Models;

public partial class RacunProizvod
{
    public int RacunId { get; set; }

    public int ProizvodId { get; set; }

    public DateTime? DatumDodavanja { get; set; }

    public virtual Proizvod Proizvod { get; set; } = null!;

    public virtual Racun Racun { get; set; } = null!;
}
