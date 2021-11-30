using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly UserManager<User> _userManager;
        private readonly CAFFShopDbContext _context;

        public CommentService(CAFFShopDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Comment> AddComment(string username, Comment comment)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;
            comment.Id = 0;
            comment.UserId = user.Id;
            var result = _context.Comment.Add(comment);
            _context.SaveChanges();
            return result.Entity;

        }

        public async Task DeleteComment(string username, int commentId)
        {
            User user = await _userManager.FindByNameAsync(username);
            bool isAdmin = await _userManager.IsInRoleAsync(user, SecurityConstants.AdminRole);
            if (isAdmin)
            {
                var result = await _context.Comment
                    .FirstOrDefaultAsync(e => e.Id == commentId);
                if (result != null)
                {
                    _context.Comment.Remove(result);
                    await _context.SaveChangesAsync();
                    return;
                }
            }
        }

        public async Task<List<Comment>> GetCommentsForCaff(string username, int caffId)
        {
            Guid? userId = (await _userManager.FindByNameAsync(username))?.Id;
            if (userId == null)
                return new List<Comment>();
            return _context.Comment.Where(c => c.CaffEntityId == caffId).ToList();


        }
    }
}
