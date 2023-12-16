namespace WannaBePrincipal.Models
{
    public class Location
    {
        public double Latitude = 0;
        public double Longitude = 0;
    }
    public class AddressData
    {
        public string City = "";
        public string Street = "";
        public string ZipCode = "";
        public string Suite = "";
        public Location Geo = new();
    }
    public class Company
    {
        public string Name = "";
        public string BS = "";
        public string CatchPhrase = "";
    }
    public class UserData
    {
        private string Id = "";
        public string Name = "";
        public string Email = "";
        public string UserName = "";
        public string Website = "";
        public string Phone = "";
        public AddressData Address = new();

        public string GetId()
        { return Id; }
        public void SetId(string id)
        { Id = id; }
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> geoDict = new()
            {
                { "latitude", Address.Geo.Latitude },
                { "longitude", Address.Geo.Longitude }
            };

            Dictionary<string, object> addressDict = new()
            {
                { "street", Address.Street },
                { "city", Address.City },
                { "zipCode", Address.ZipCode },
                { "suite", Address.Suite },
                { "geo", geoDict }
            };

            Dictionary<string, object> userData = new()
            {
                { "name", Name },
                { "email", Email },
                { "username", UserName },
                { "website", Website },
                { "address",  addressDict},
                { "phone",  Phone}
            };
            return userData;
        }
    }
}
