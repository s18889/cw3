using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class StOb
    {
        public int StanowiskoIdStano { get; set; }
        public int ObowiazkiIdObowia { get; set; }

        public virtual Obowiazki ObowiazkiIdObowiaNavigation { get; set; }
        public virtual Stanowisko StanowiskoIdStanoNavigation { get; set; }
    }
}
