
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        public Caff CaffFile { get; set; }
        public int CaffFileId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
