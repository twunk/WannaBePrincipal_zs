using System.ComponentModel.DataAnnotations;

namespace WannaBePrincipal.Models
{
    public class Location
    {
        [Required]
        public double Lat { get; set; }

        [Required]
        public double Lng { get; set; }
    }

    public class AddressData
    {
        [Required]
        public required string City { get; set; }

        [Required]
        public required string Street { get; set; }

        [Required]
        public required string ZipCode { get; set; }

        [Required]
        public required string Suite { get; set; }

        [Required]
        public required Location Geo { get; set; }
    }

    public class CompanyData
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string BS { get; set; }

        [Required]
        public required string CatchPhrase { get; set; }
    }

    public class UserData
    {
        public string Id = "";

        [Required]
        public required string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9+_.-]+@(.+)$", ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Website { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required CompanyData Company { get; set; }

        [Required]
        public required AddressData Address { get; set; }

        public string GetId()
        { return Id; }
        public void SetId(string id)
        { Id = id; }
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> geoDict = new()
            {
                { "lat", Address.Geo.Lat },
                { "lng", Address.Geo.Lng }
            };

            Dictionary<string, object> addressDict = new()
            {
                { "street", Address.Street },
                { "city", Address.City },
                { "zipCode", Address.ZipCode },
                { "suite", Address.Suite },
                { "geo", geoDict }
            };

            Dictionary<string, object> companyDict = new()
            {
                { "bs", Company.BS },
                { "catchPharse", Company.CatchPhrase },
                { "name", Company.Name }
            };

            Dictionary<string, object> userData = new()
            {
                { "name", Name },
                { "email", Email },
                { "username", UserName },
                { "website", Website },
                { "address",  addressDict},
                { "company", companyDict },
                { "phone",  Phone}
            };
            return userData;
        }
    }
}
