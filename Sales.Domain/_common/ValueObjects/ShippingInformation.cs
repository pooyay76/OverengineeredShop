namespace Sales.Domain._common.ValueObjects
{
    public record ShippingInformation
    {
        public string ReceiverFirstName { get; init; }
        public string ReceiverLastName { get; init; }


        public ShippingType ShippingType { get; init; }
        public string Country { get; init; }
        public string Province { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public string PostalCode { get; init; }
        public string ContactPhoneNumber { get; init; }

        internal ShippingInformation(string receiverFirstName, string receiverLastName, string country, string province,
            string city, string address, string postalCode, string contactPhoneNumber)
        {
            ReceiverFirstName = receiverFirstName;
            ReceiverLastName = receiverLastName;
            Country = country;
            Province = province;
            City = city;
            Address = address;
            PostalCode = postalCode;
            ContactPhoneNumber = contactPhoneNumber;
        }
    }
}
