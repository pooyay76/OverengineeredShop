namespace Sales.Application.DTOs
{
    public class CommandResponse
    {
        public bool IsSuccessful { get; set; } = false;
        public List<string> Errors { get; init; } = [];
        public CommandResponse Succeeded()
        {
            IsSuccessful = true;
            return this;
        }
        public CommandResponse Failed(List<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccessful = false;
            return this;
        }
        public CommandResponse Failed(string error)
        {
            Errors.Add(error);
            IsSuccessful = false;
            return this;
        }
    }
}
