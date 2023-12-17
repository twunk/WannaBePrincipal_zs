using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    public class UserControllerTests
    {
        /*static private string jsonData = @"{
              ""address"": {
                ""city"": ""Gwenborough"",
                ""geo"": {
                  ""lat"": ""-37.3159"",
                  ""lng"": ""81.1496""
                },
                ""street"": ""Kulas Light"",
                ""suite"": ""Apt. 556"",
                ""zipcode"": ""92998-3874""
              },
              ""company"": {
                ""bs"": ""harness real-time e-markets"",
                ""catchPhrase"": ""Multi-layered client-server neural-net"",
                ""name"": ""Romaguera-Crona""
              },
              ""email"": ""Sincere@april.biz"",
              ""name"": ""Leanne Graham"",
              ""phone"": ""1-770-736-8031 x56442"",
              ""username"": ""Bret"",
              ""website"": ""hildegard.org""
            }";

        static private string badJsonData = @"{
              ""address"": {
                ""city"": ""Gwenborough"",
                ""geo"": {
                  ""lat"": ""-37.3159"",
                  ""lng"": ""81.1496""
                },
                ""street"": ""Kulas Light"",
                ""suite"": ""Apt. 556"",
                ""zipcode"": ""92998-3874""
              },
              ""company"": {
                ""bs"": ""harness real-time e-markets"",
                ""catchPhrase"": ""Multi-layered client-server neural-net"",
                ""name"": ""Romaguera-Crona""
              },
              ""email"": ""Sincereapril.biz"",
              ""name"": ""Leanne Graham"",
              ""phone"": ""1-770-736-8031 x56442"",
              ""username"": ""Bret""
            }";

        [Fact]
        public async Task Create_WithValidUserData_ReturnsCreatedResult()
        {
            // Arrange
            var controller = CreateUserController();
            
            UserData user = JsonConvert.DeserializeObject<UserData>(jsonData);
            
            // Act
            var result = await controller.Create(user);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_WithInvalidUserData_ReturnsBadRequestResult()
        {
            // Arrange
            var controller = CreateUserController();
            UserData invalidUserData = new UserData
            {
                Name = "Leanne Graham",
                Email = "",
                Phone = "1-770-736-8031 x56442",
                UserName = "Bret",
                Website = "hildegard.org",
                Address = new AddressData
                {
                    City = "Gwenborough",
                    Street = "Kulas Light",
                    Suite = "Apt. 556",
                    ZipCode = "92998-3874",
                    Geo = new Location
                    {
                        Lat = -37.3159,
                        Lng = 81.1496
                    }
                },
                Company = new CompanyData
                {
                    BS = "harness real-time e-markets",
                    CatchPhrase = "Multi-layered client-server neural-net",
                    Name = "Romaguera-Crona"
                },
            };

            // Act
            var result = await controller.Create(invalidUserData);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            // Arrange
            var controller = CreateUserController();
            
            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Add more test methods for other controller actions (Edit, Delete, etc.)

        private static UserController CreateUserController()
        {
            // You can create an instance of UserController here and inject any necessary dependencies with test db
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Environment.CurrentDirectory + "/../../../../WannaBePrincipal/keys/google_credentials_unit_test.json");

            return new UserController("wannabe-unit-test");
        }*/
    }
}
