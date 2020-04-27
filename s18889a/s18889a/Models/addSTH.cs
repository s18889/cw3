using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s18889a.Models;

namespace s18889a.Models
{
    public class addSTH
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Deadline { get; set; }
        public String IdTeam { get; set; }
        public String IdAssignedTo { get; set; }
        public String IdCreator { get; set; }
        public TAskType  TaskType { get; set; }
    }
}
