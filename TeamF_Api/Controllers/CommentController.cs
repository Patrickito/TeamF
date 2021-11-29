using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        private readonly ILogger<CaffController> _logger;

        public CommentController(ICommentService service, ILogger<CaffController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("CaffFile/{id}", Name = "GetCaffFileComment")]
        [ProducesResponseType(typeof(List<Comment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<Comment>>> GetCaffFileComment(int id)
        {
            return await _service.GetCommentsForCaff(User.Identity.Name, id);
        }

        [Authorize(Policy = SecurityConstants.AdminPolicy)]
        [HttpDelete("{id}", Name = "DeleteCaffFileComment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {

            await _service.DeleteComment(User.Identity.Name, id);
            _logger.LogInformation($"delete commend id: {id}");
            return NoContent();
        }

        [Authorize]
        [HttpPost(Name = "AddCaffFileComment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Comment>> AddComment([FromBody] Comment newComment)
        {
            _logger.LogDebug($"add comment: {newComment}");
            return await _service.AddComment(User.Identity.Name, newComment);
        }

    }
}
