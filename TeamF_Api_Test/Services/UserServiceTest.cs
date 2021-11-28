using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security.Token;
using TeamF_Api.Services.Implementations;
using TeamF_Api.Services.Interfaces;
using Xunit;

namespace TeamF_Api_Test.Services
{
    public class UserServiceTest : IDisposable
    {
        private readonly CAFFShopDbContext _context;
        private readonly IUserService _service;
        private readonly Mock<ITokenGenerator> _tokenGenerator;
        private readonly Mock<UserManager<User>> _userManager;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<CAFFShopDbContext>()
                .UseInMemoryDatabase(databaseName: "testUserContext")
                .Options;

            _context = new CAFFShopDbContext(options);
            _tokenGenerator = new Mock<ITokenGenerator>();
            _userManager = MockUserManager(new List<User>());
            _service = new UserService(_tokenGenerator.Object, _userManager.Object, _context);

            _context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
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
        public async void TestRegistration()
        {
            var newName = "userName";
            var newPassword = "testUser";

            await _service.RegisterUser(newName, newPassword);

            var user = new User
            {
                UserName = newName,
                Roles = new List<Role>
                {
                    new Role { Id = 1L, Name = "BaseUser" },
                    new Role { Id = 2L, Name = "Administrator" }
                }
            };

            _userManager.Verify(um => um.CreateAsync(user, newPassword), Times.Once());
        }
    }
}
