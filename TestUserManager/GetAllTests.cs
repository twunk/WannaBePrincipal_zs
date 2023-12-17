using Microsoft.AspNetCore.Mvc;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    public class GetAllTests
    {
        private Mock<IUserModel> _userModelMock;

        public GetAllTests()
        {
            _userModelMock = new Mock<IUserModel>();
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            // Arrange
            _userModelMock.Setup(x => x.GetUsersFromDB()).ReturnsAsync(new List<UserData>());
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<UserData>>(okResult.Value);
            Assert.Empty(users);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUsers()
        {
            // Arrange
            var mockUsers = new List<UserData>
            {
                HelpFunctions.CreateUserData(),
                HelpFunctions.CreateUserData()
            };
            _userModelMock.Setup(x => x.GetUsersFromDB()).ReturnsAsync(mockUsers);
            var controller = new UserController(_userModelMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<UserData>>(okResult.Value);
            Assert.Equal(mockUsers.Count, users.Count);
        }

        [Fact]
        public async Task GetAll_ThrowsException()
        {
            // Arrange
            _userModelMock.Setup(x => x.GetUsersFromDB()).ThrowsAsync(new Exception("Some error"));
            var controller = new UserController(_userModelMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => controller.GetAll());
        }

    }
}
