using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Img
    {
        public int Id { get; set; }
        public virtual CaffEntity Caff { get; set; }
        public int CaffId { get; set; }
        public string Caption { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public string Address { get; set; }

    }
}
