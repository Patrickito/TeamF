using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("CaffFile/{id}")]
        public async Task<ActionResult<List<Comment>>> GetCaffFileComment(int id)
        {
            return await _service.GetCommentsForCaff(User.Identity.Name, id);
        }

        [Authorize(Policy = SecurityConstants.AdminPolicy)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteComment(User.Identity.Name, id);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] Comment newComment)
        {
            return await _service.AddComment(User.Identity.Name, newComment);
        }

    }
}
