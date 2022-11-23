using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostContent : Attach
    {

       
        public string? Comment { get; set; }
        public bool like { get; set; } = false;
        public int? Countlike { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual Post Post { get; set; } = null!;


    }
}
