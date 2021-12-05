using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Services.Implementations;
using TeamF_Api.Services.Interfaces;
using Xunit;

namespace TeamF_Api_Test.Services
{
    public class CaffServiceTest : IDisposable
    {
        private readonly CAFFShopDbContext _context;
        private readonly ICaffService _service;
        private readonly Mock<UserManager<User>> _userManager;

        private User testUser;
        private CaffEntity testCaff;
        private Img testImg;

        public CaffServiceTest()
        {
            var options = new DbContextOptionsBuilder<CAFFShopDbContext>()
                .UseInMemoryDatabase(databaseName: "testCaffContext")
                .Options;

            _context = new CAFFShopDbContext(options);
            _userManager = MockUserManager(new List<User>());
            _service = new CaffService(_context, _userManager.Object);

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

            testImg = new Img
            {
                Id = 1,
                Address = "addr",
                Caption = "cap",
                Tags = new List<Tag>()
            };

            testCaff = new CaffEntity
            {
                Id = 1,
                Address = "testAddress",
                Comments = new List<Comment>(),
                Images = new List<Img> { testImg },
                Owner = testUser,
                OwnerId = testUser.Id
            };

            _context.CaffEntity.Add(testCaff);
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
        public async void TestCaffentityInsertion()
        {
            var newEntity = new CaffEntity
            {
                Id = 2,
                Address = "testAddress",
                Comments = new List<Comment>(),
                Images = new List<Img>(),
                Owner = testUser,
                OwnerId = testUser.Id
            };

            await _service.AddCaffAsync(newEntity);

            var res = _context.CaffEntity.Contains(newEntity);

            Assert.True(res);
        }

        [Fact]
        public async void TestGetCaffById()
        {
            var result = await _service.GetCaff(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("testAddress", result.Address);
            Assert.Empty(result.Comments);
            Assert.Equal(1, result.Images.Count);
            Assert.Equal(testUser.Id, result.Owner.Id);
            Assert.Equal(testUser.Id, result.OwnerId);
        }

        [Fact]
        public async void TestGetCaffs()
        {
            var result = await _service.GetCaffs();

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public async void TestGetImage()
        {
            var res = await _service.GetImg(1);

            Assert.Equal(testImg.Address, res.Address);
        }
    }
}
