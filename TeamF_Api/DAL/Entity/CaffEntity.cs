using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class CaffEntity
    {
        public int Id { get; set; }
        public virtual User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Place { get; set; }
        public virtual ICollection<Img> Images { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
