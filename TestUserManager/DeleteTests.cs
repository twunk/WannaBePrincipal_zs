using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WannaBePrincipal.Controllers;

namespace TestUserManager
{
    public class DeleteTests
    {
        private Mock<IUserModel> _userModelMock;

        public DeleteTests()
        {
            _userModelMock = new Mock<IUserModel>();
        }

        [Fact]
        public async Task Delete_WithNullId_ReturnsBadRequest()
        {
            // Arrange
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_DeletesUser()
        {
            // Arrange
            _userModelMock.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(true);
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.Delete("valid-id");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithNonExistingUser_ReturnsNotFound()
        {
            // Arrange
            _userModelMock.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(false);
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.Delete("non-existing-id");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Problem occurred while editing the non-existing-id user.", notFoundResult.Value);
        }
    }
}
