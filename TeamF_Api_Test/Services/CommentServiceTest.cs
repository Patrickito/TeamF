using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Services.Implementations;
using TeamF_Api.Services.Interfaces;
using Xunit;

namespace TeamF_Api_Test.Services
{
    public class CommentServiceTest : IDisposable
    {
        private readonly CAFFShopDbContext _context;
        private readonly ICommentService _service;
        private readonly Mock<UserManager<User>> _userManager;

        private User testUser;
        private CaffEntity testCaff;
        private Comment testComment;

        public CommentServiceTest()
        {
            var options = new DbContextOptionsBuilder<CAFFShopDbContext>()
                .UseInMemoryDatabase(databaseName: "testCommentContext")
                .Options;

            _context = new CAFFShopDbContext(options);
            _userManager = MockUserManager(new List<User>());
            _service = new CommentService(_context, _userManager.Object);

            _context.Database.EnsureCreated();

            AddTestData();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void AddTestData()
        {
            testUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "user",
                NormalizedUserName = "USER"
            };

            testCaff = new CaffEntity
            {
                Id = 1,
                Address = "addr",
                Creator = "user",
                Comments = new List<Comment>(),
                Images = new List<Img>(),
                Owner = testUser,
                OwnerId = testUser.Id
            };

            testComment = new Comment
            {
                Id = 1,
                CommentText = "test",
                DateTime = DateTime.Now,
                User = testUser,
                UserId = testUser.Id,
                CaffEntity = testCaff,
                CaffEntityId = testCaff.Id
            };

            _context.Users.Add(testUser);
            _context.CaffEntity.Add(testCaff);
            _context.Comment.Add(testComment);
            _context.SaveChanges();
        }

        private Mock<UserManager<User>> MockUserManager(List<User> ls)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        [Fact]
        public async void TestAddComment()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);

            var comment = new Comment
            {
                User = testUser,
                UserId = testUser.Id,
                CaffEntity = testCaff,
                CaffEntityId = testCaff.Id,
                CommentText = "new comment",
                DateTime = DateTime.Now
            };

            var result = await _service.AddComment(testUser.UserName, comment);

            var data = _context.Comment.ToList();

            Assert.Equal(2, data.Count);
            Assert.Equal(testUser.Id, data[1].UserId);
            Assert.Equal(testCaff.Id, data[1].CaffEntityId);
            Assert.Equal("new comment", data[1].CommentText);
            Assert.Equal(testUser.Id, result.UserId);
            Assert.Equal(testCaff.Id, result.CaffEntityId);
            Assert.Equal("new comment", result.CommentText);
        }

        [Fact]
        public async void TestAddCommentInvalidUser()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var comment = new Comment
            {
                User = testUser,
                UserId = testUser.Id,
                CaffEntity = testCaff,
                CaffEntityId = testCaff.Id,
                CommentText = "new comment",
                DateTime = DateTime.Now
            };

            var result = await _service.AddComment(testUser.UserName, comment);

            var data = _context.Comment.ToList();

            Assert.Single(data);
            Assert.Null(result);
        }

        [Fact]
        public async void TestDeleteUser()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            _userManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            await _service.DeleteComment(testUser.UserName, 1);

            var data = _context.Comment.ToList();

            Assert.Empty(data);
        }

        [Fact]
        public async void TestDeleteUserUnauthorized()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            _userManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            await _service.DeleteComment(testUser.UserName, 1);

            var data = _context.Comment.ToList();

            Assert.Single(data);
        }

        [Fact]
        public async void TestGetCommentsForCaff()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            _userManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            var result = await _service.GetCommentsForCaff(testUser.UserName, 1);

            Assert.Single(result);
            Assert.Equal(testUser.Id, result[0].UserId);
            Assert.Equal(testCaff.Id, result[0].CaffEntityId);
            Assert.Equal("test", result[0].CommentText);
        }

        [Fact]
        public async void TestGetCommentsForCaffEmpty()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            _userManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            var result = await _service.GetCommentsForCaff(testUser.UserName, 2);

            Assert.Empty(result);
        }
    }
}
