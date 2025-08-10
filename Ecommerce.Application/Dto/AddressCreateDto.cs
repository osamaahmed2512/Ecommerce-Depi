

namespace Ecommerce.Application.Dto
{
    public class AddressCreateDto
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
