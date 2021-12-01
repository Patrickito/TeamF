using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using TeamF_Api.Controllers;
using TeamF_Api.DTO;
using TeamF_Api.Services.Exceptions;
using TeamF_Api.Services.Interfaces;
using Xunit;

namespace TeamF_Api_Test.Services
{
    public class AuthenticationControllerTest
    {
        private readonly AuthenticationController _controller;
        private readonly Mock<IUserService> _userService;

        public AuthenticationControllerTest()
        {
            _userService = new Mock<IUserService>();
            _controller = new AuthenticationController(_userService.Object, NullLogger<AuthenticationController>.Instance);
        }

        [Fact]
        public async void TestRegistration()
        {
            var newUser = new AuthenticationDto { UserName = "name", Password = "password" };
            var res = await _controller.Register(newUser);

            Assert.IsType<NoContentResult>(res);
            _userService.Verify(us => us.RegisterUser(It.Is<string>(n => n.Equals("name")), It.Is<string>(n => n.Equals("password"))), Times.Once());
        }

        [Fact]
        public async void TestRegistrationConflictingNames()
        {
            _userService.Setup(us => us.RegisterUser(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new ConflictingUserNameException("test"));

            var newUser = new AuthenticationDto { UserName = "name", Password = "password" };
            var res = await _controller.Register(newUser);

            Assert.IsType<BadRequestResult>(res);
            _userService.Verify(us => us.RegisterUser(It.Is<string>(n => n.Equals("name")), It.Is<string>(n => n.Equals("password"))), Times.Once());
        }

        [Fact]
        public async void TestLogin()
        {
            _userService.Setup(us => us.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("testToken");

            var userData = new AuthenticationDto { UserName = "name", Password = "password" };
            var res = await _controller.Login(userData);
            var data = (res as OkObjectResult).Value as TokenDTO;

            Assert.IsType<OkObjectResult>(res);
            Assert.Equal("testToken", data.Token);
            _userService.Verify(us => us.Login(It.Is<string>(n => n.Equals("name")), It.Is<string>(n => n.Equals("password"))), Times.Once());
        }

        [Fact]
        public async void TestInvalidLogin()
        {
            _userService.Setup(us => us.Login(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new AuthenticationException("test"));

            var userData = new AuthenticationDto { UserName = "name", Password = "password" };
            var res = await _controller.Login(userData);

            Assert.IsType<UnauthorizedResult>(res);
            _userService.Verify(us => us.Login(It.Is<string>(n => n.Equals("name")), It.Is<string>(n => n.Equals("password"))), Times.Once());
        }

        [Fact]
        public async void TestChangeRoles()
        {
            _userService.Setup(us => us.UpdateRoles(It.IsAny<string>(), It.IsAny<ICollection<string>>()));

            var request = new RoleChangeDTO { UserName = "user", Roles = new string[] { "role1", "role2" } };
            var res = await _controller.ChangeRoles(request);

            Assert.IsType<NoContentResult>(res);
            _userService.Verify(us => us.UpdateRoles(It.Is<string>(n => n.Equals("user")), It.Is<ICollection<string>>(r => r.Count == 2)), Times.Once());
        }

        [Fact]
        public async void TestGetUsers()
        {
            var data = new List<UserDTO>
            {
                new UserDTO{Id = Guid.NewGuid().ToString(), UserName = "user", Roles = new string[0] }
            };
            _userService.Setup(us => us.FetchAllUsers()).ReturnsAsync(data);

            var res = await _controller.GetAllUsers();
            var resData = (res as OkObjectResult).Value as ICollection<UserDTO>;

            Assert.Equal("user", resData.First().UserName);
            _userService.Verify(us => us.FetchAllUsers(), Times.Once());
        }

        [Fact]
        public async void TestDeleteUser()
        {
            _userService.Setup(us => us.DeleteUser(It.IsAny<string>()));

            var res = await _controller.DeleteUser("test");

            Assert.IsType<NoContentResult>(res);
            _userService.Verify(us => us.DeleteUser(It.Is<string>(n => n.Equals("test"))), Times.Once());
        }

        [Fact]
        public async void TestGetRoles()
        {
            var data = new List<string> { "role1" };
            _userService.Setup(us => us.FetchAllRoles()).ReturnsAsync(data);

            var res = await _controller.GetAllRoles();
            var resData = (res as OkObjectResult).Value as ICollection<string>;

            Assert.Equal("role1", resData.First());
            _userService.Verify(us => us.FetchAllRoles(), Times.Once());
        }
    }
}
