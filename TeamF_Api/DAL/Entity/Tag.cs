using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public virtual Img Img { get; set; }
        public int ImgId { get; set; }

    }
}
