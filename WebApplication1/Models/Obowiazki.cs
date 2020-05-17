using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Obowiazki
    {
        public Obowiazki()
        {
            StOb = new HashSet<StOb>();
        }

        public int IdObowaizk { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<StOb> StOb { get; set; }
    }
}
