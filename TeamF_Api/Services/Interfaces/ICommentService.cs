using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsForCaff(string username, int caffId);
        Task<Comment> AddComment(string username, Comment comment);
        Task DeleteComment(string username, int commentId);
    }
}
