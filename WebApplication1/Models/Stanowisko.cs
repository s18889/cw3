using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Stanowisko
    {
        public Stanowisko()
        {
            StOb = new HashSet<StOb>();
        }

        public int IdStanowisk { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<StOb> StOb { get; set; }
    }
}
