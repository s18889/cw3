using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Osoba
    {
        public Osoba()
        {
            Dostawca = new HashSet<Dostawca>();
            InversePrzelozonyNavigation = new HashSet<Osoba>();
        }

        public int Pesel { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int? Stanowisko { get; set; }
        public int? Przelozony { get; set; }
        public int? Pensja { get; set; }

        public virtual Osoba PrzelozonyNavigation { get; set; }
        public virtual ICollection<Dostawca> Dostawca { get; set; }
        public virtual ICollection<Osoba> InversePrzelozonyNavigation { get; set; }
    }
}
