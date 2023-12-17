using Microsoft.AspNetCore.Mvc;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    public class EditTests
    {
        private UserController _controller;
        private Mock<IUserModel> _userModelMock;

        public EditTests()
        {
            _userModelMock = new Mock<IUserModel>();
            _controller = new UserController(_userModelMock.Object);
        }

        // Tests for GET Edit

        [Fact]
        public async Task GetEdit_WithNullId_ReturnsBadRequest()
        {
            var result = await _controller.Edit(null as string);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetEdit_WithNonExistingUser_ReturnsNotFound()
        {
            _userModelMock.Setup(x => x.GetUser(It.IsAny<string>())).ThrowsAsync(new KeyNotFoundException($"User with ID some-id not found."));

            var result = await _controller.Edit("some-id");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetEdit_WithValidId_ReturnsOk()
        {
            _userModelMock.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(HelpFunctions.CreateUserData());

            var result = await _controller.Edit("valid-id");

            Assert.IsType<OkObjectResult>(result);
        }

        // Tests for POST Edit

        [Fact]
        public async Task PostEdit_WithNullId_ReturnsBadRequest()
        {
            var result = await _controller.Edit(null as string, HelpFunctions.CreateUserData());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithInvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("error", "bad format");

            var result = await _controller.Edit("valid-id", HelpFunctions.CreateInvalidUserData());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithEditFailure_ReturnsNotFound()
        {
            _userModelMock.Setup(x => x.EditUser(It.IsAny<string>(), It.IsAny<UserData>())).ReturnsAsync(false);

            var result = await _controller.Edit("valid-id", HelpFunctions.CreateUserData());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithSuccessfulEdit_ReturnsOk()
        {
            _userModelMock.Setup(x => x.EditUser(It.IsAny<string>(), It.IsAny<UserData>())).ReturnsAsync(true);

            var result = await _controller.Edit("valid-id", HelpFunctions.CreateUserData());

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
