using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Dostawca
    {
        public Dostawca()
        {
            Material = new HashSet<Material>();
        }

        public int Nip { get; set; }
        public string Nazwa { get; set; }
        public int? NrTelefinu { get; set; }
        public string EMail { get; set; }
        public int? OsobaOdpowiedzialnaZaKomunik { get; set; }

        public virtual Osoba OsobaOdpowiedzialnaZaKomunikNavigation { get; set; }
        public virtual ICollection<Material> Material { get; set; }
    }
}
