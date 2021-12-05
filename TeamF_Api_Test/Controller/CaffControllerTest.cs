using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TeamF_Api.Controllers;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Services.Interfaces;
using Xunit;

namespace TeamF_Api_Test.Controller
{
    public class CaffControllerTest
    {
        private readonly Mock<ICaffService> _service;
        private readonly CaffController _controller;

        public CaffControllerTest()
        {
            _service = new Mock<ICaffService>();
            _controller = new CaffController(MockUserManager(new List<User>()).Object, _service.Object, NullLogger<CaffController>.Instance);
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
        public async void TestDeleteCaff()
        {
            var result = await _controller.Delete(1);

            _service.Verify(s => s.DeleteCaffAsync(It.Is<int>(id => id == 1)), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void TestGetCaffs()
        {
            var testData = new CaffEntity
            {
                Address = "addr",
                Comments = null,
                Creator = null,
                Id = 1,
                Images = null,
                Owner = null,
                OwnerId = Guid.NewGuid()
            };

            _service.Setup(s => s.GetCaffs()).ReturnsAsync(new List<CaffEntity> { testData });

            await _controller.ListAllCaff();

            _service.Verify(s => s.GetCaffs(), Times.Once());
        }
    }
}
