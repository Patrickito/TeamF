
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        public virtual CaffEntity CaffEntity { get; set; }
        public int CaffEntityId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
