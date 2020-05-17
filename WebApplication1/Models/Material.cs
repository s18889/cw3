using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Material
    {
        public int IdMaterial { get; set; }
        public string Nazwa { get; set; }
        public int? CalkowiteZapotrzebowanie { get; set; }
        public string Jednostka { get; set; }
        public int? ZapotrzebowanieNastepnyTyd { get; set; }
        public int? DostawcaNip { get; set; }

        public virtual Dostawca DostawcaNipNavigation { get; set; }
    }
}
