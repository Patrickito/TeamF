using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaffController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly ICaffService _service;

        public CaffController(UserManager<User> userManager, ICaffService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost(Name = "UploadCaffFIle")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];

                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid() +
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Parser parser = new Parser();

                    Caff caff;
                    string path = fullPath;
                    using (caff = parser.parseCaff(path))
                    {
                        int cnt = 0;

                        List<Img> images = new List<Img>();

                        for (uint k = 0; k < caff.getAnimationNumber(); k++)
                        {
                            var item = caff.getCaffAnimation(k);
                            string caption = string.Join("", item.getCaption());
                            string[] tags = string.Join("", item.getTags()).Split('\0')[..^1];
                            List<Tag> tagList = new List<Tag>();

                            foreach (var tagitem in tags)
                            {
                                tagList.Add(new Tag() { TagName = tagitem });
                            }



                            var Width = item.getWidth();
                            var Height = item.getHeight();
                            Bitmap bitmap = new Bitmap((int)Width, (int)Height);
                            for (ulong i = 0; i < Width; i++)
                            {
                                for (ulong j = 0; j < Height; j++)
                                {
                                    using (var p = item.getPixelAt(i, j))
                                    {
                                        Color color = Color.FromArgb(p.r, p.g, p.b);
                                        bitmap.SetPixel((int)i, (int)j, color);
                                    }
                                }
                            }
                            string eleres = Path.Combine("Resources", "Previews", "preview_" + cnt + "_" + fileName + ".jpg");
                            bitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), eleres), ImageFormat.Jpeg);

                            Img img = new Img()
                            {
                                Caption = caption,
                                Tags = tagList,
                                Address = eleres
                            };
                            images.Add(img);

                            cnt++;

                        }
                        string creator = string.Join("", caff.getCreator());
                        CaffEntity ce = new CaffEntity()
                        {
                            Creator = creator,
                            Images = images,
                            OwnerId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id,
                            Address = dbPath
                        };

                        await _service.AddCaffAsync(ce);

                        caff.Dispose();
                    }
                    parser.Dispose();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteCaffFile")]
        [Authorize(Policy = SecurityConstants.AdminPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteCaffAsync(id);
            return NoContent();
        }

        [HttpGet(Name = "GetAllCaffFiles")]
        [Authorize]
        [ProducesResponseType(typeof(List<CaffEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<CaffEntity>>> ListAllCaff()
        {
            return await _service.GetCaffs();
        }

        [HttpGet("CaffFile/{id}", Name = "GetCaffFile")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("image/caff")]
        public async Task<ActionResult> GetCaffFile(int id)
        {
            CaffEntity ce = await _service.GetCaff(id);
            Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), ce.Address));
            return File(b, "image/caff");
        }

        [HttpGet("ImgFile/{id}", Name = "GetImgFile")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/octet-stream")]
        public async Task<ActionResult> GetImgFile(int id)
        {
            Img ce = await _service.GetImg(id);
            Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), ce.Address));
            return File(b, "application/octet-stream");
        }

    }
}

