using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Caff
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Place { get; set; }
        public ICollection<Img> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }


    }
}
