using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
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
                UserName = newName
            };

            _userManager.Verify(um => um.CreateAsync(It.Is<User>(u => u.Id.Equals(user.Id)), It.Is<string>(p => p.Equals("testUser"))), Times.Once());
        }

        [Fact]
        public async void TestChaingePassword()
        {
            var user = new User
            {
                UserName = "newName"
            };
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            await _service.ChangePassword("newName", "oldPassword", "newPassword");

            _userManager.Verify(um => um.FindByNameAsync(It.Is<string>(n => n.Equals("newName"))), Times.Once());
            _userManager.Verify(um => um.ChangePasswordAsync(It.Is<User>(n => n.Equals(user)), It.Is<string>(p => p.Equals("oldPassword")), It.Is<string>(p => p.Equals("newPassword"))), Times.Once());
        }

        [Fact]
        public async void TestDeleteUser()
        {
            var user = new User
            {
                UserName = "newName"
            };
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            await _service.DeleteUser("test")
                ;
            _userManager.Verify(um => um.FindByIdAsync(It.Is<string>(n => n.Equals("test"))), Times.Once());
            _userManager.Verify(um => um.DeleteAsync(It.Is<User>(n => n.Equals(user))), Times.Once());
        }

        [Fact]
        public async void TestFindUserByName()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var res = await _service.FindUserByName("name");

            Assert.Equal(user.Id.ToString(), res.Id.ToString());
            Assert.Equal("name", res.UserName);
            _userManager.Verify(um => um.FindByNameAsync(It.Is<string>(n => n.Equals("name"))));
        }

        [Fact]
        public async void TestAddRole()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(um => um.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { SecurityConstants.BaseUserRole });

            var roles = new List<string> { SecurityConstants.BaseUserRole, SecurityConstants.AdminRole };
            await _service.UpdateRoles("name", roles);

            _userManager.Verify(um => um.AddToRoleAsync(It.Is<User>(n => n.Equals(user)), It.Is<string>(r => r.Equals(SecurityConstants.AdminRole))), Times.Once());
            _userManager.Verify(um => um.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void TestRemoveRole()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(um => um.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { SecurityConstants.BaseUserRole, SecurityConstants.AdminRole });

            var roles = new List<string> { SecurityConstants.BaseUserRole };
            await _service.UpdateRoles("name", roles);

            _userManager.Verify(um => um.RemoveFromRoleAsync(It.Is<User>(n => n.Equals(user)), It.Is<string>(r => r.Equals(SecurityConstants.AdminRole))), Times.Once());
            _userManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void TestLogin()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            _userManager.Setup(um => um.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string> { SecurityConstants.BaseUserRole });
            _tokenGenerator.Setup(tg => tg.GenerateToken(It.IsAny<TokenData>())).Returns("token");

            var token = await _service.Login("name", "password");

            Assert.Equal("token", token);
            _userManager.Verify(um => um.FindByNameAsync(It.Is<string>(n => n.Equals("name"))), Times.Once());
            _userManager.Verify(um => um.CheckPasswordAsync(It.Is<User>(u => u.Equals(user)), It.Is<string>(n => n.Equals("password"))), Times.Once());
            _userManager.Verify(um => um.GetRolesAsync(It.Is<User>(u => u.Equals(user))), Times.Once());
            _tokenGenerator.Verify(tg => tg.GenerateToken(It.IsAny<TokenData>()), Times.Once());
        }

        [Fact]
        public async void TestLoginUserNotFound()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            await Assert.ThrowsAsync<AuthenticationException>(() => _service.Login("name", "password"));

            _userManager.Verify(um => um.FindByNameAsync(It.Is<string>(n => n.Equals("name"))), Times.Once());
            _userManager.Verify(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
            _userManager.Verify(um => um.GetRolesAsync(It.IsAny<User>()), Times.Never());
            _tokenGenerator.Verify(tg => tg.GenerateToken(It.IsAny<TokenData>()), Times.Never());
        }

        [Fact]
        public async void TestLoginIncorrectPassword()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "name",
                NormalizedUserName = "NAME"
            };
            _userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            await Assert.ThrowsAsync<AuthenticationException>(() => _service.Login("name", "password"));

            _userManager.Verify(um => um.FindByNameAsync(It.Is<string>(n => n.Equals("name"))), Times.Once());
            _userManager.Verify(um => um.CheckPasswordAsync(It.Is<User>(u => u.Equals(user)), It.Is<string>(n => n.Equals("password"))), Times.Once());
            _userManager.Verify(um => um.GetRolesAsync(It.IsAny<User>()), Times.Never());
            _tokenGenerator.Verify(tg => tg.GenerateToken(It.IsAny<TokenData>()), Times.Never());
        }
    }
}
