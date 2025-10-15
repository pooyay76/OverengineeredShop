namespace Sales.Api.Contracts.OrderContracts.Commands
{
    public class SubmitOrderCommand
    {
        public Guid UserId { get; set; }

        public string ReceiverCountry { get; set; }
        public string ReceiverProvince { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPostalCode { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverContactNumber { get; set; }

    }
}
