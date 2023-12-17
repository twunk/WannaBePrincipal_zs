using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    public class CreateTests
    {
        private Mock<IUserModel> _userModelMock;

        public CreateTests()
        {
            _userModelMock = new Mock<IUserModel>();
        }

        [Fact]
        public async Task Create_WithValidUserData_ReturnsCreatedResult()
        {
            // Arrange
            var validUserData = HelpFunctions.CreateUserData();
            _userModelMock.Setup(x => x.AddUser(It.IsAny<UserData>())).ReturnsAsync("some-id");
            var controller = new UserController(_userModelMock.Object);
            controller.ModelState.Clear();

            // Act
            var result = await controller.Create(validUserData);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("some-id", createdResult.ActionName); // or whatever is appropriate
            Assert.Equal(validUserData, createdResult.Value);
        }

        [Fact]
        public async Task Create_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var invalidUserData = HelpFunctions.CreateInvalidUserData();
            var controller = new UserController(_userModelMock.Object);
            controller.ModelState.AddModelError("error", "bad format");

            // Act
            var result = await controller.Create(invalidUserData);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_FailsToAddUser_ReturnsBadRequest()
        {
            // Arrange
            var validUserData = HelpFunctions.CreateUserData();
            _ = _userModelMock.Setup(x => x.AddUser(It.IsAny<UserData>())).ReturnsAsync((string)null); // Assuming null indicates failure
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.Create(validUserData);

            // Assert
            Assert.IsType<BadRequestResult>(result); // or whatever is appropriate
        }
    }
}
