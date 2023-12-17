using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    public static class HelpFunctions
    {
        public static UserData CreateUserData()
        {
            return new UserData
            {
                Name = "Leanne Graham",
                Email = "test@domain.com",
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
        }

        public static UserData CreateInvalidUserData()
        {
            return new UserData
            {
                Name = "Leanne Graham",
                Email = "test@domain.com",
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
        }
    }
}
