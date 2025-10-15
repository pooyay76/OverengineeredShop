namespace Sales.Application.DTOs
{
    public class CommandResponseGeneric<T>
    {
        public bool IsSuccessful { get; private set; } = false;
        public List<string> Errors { get; private set; } = [];
        public T Result { get; private set; }
        public CommandResponseGeneric<T> Succeeded(T result)
        {
            if (Errors.Count == 0)
            {
                Result = result;
                IsSuccessful = true;
            }
            return this;
        }
        public CommandResponseGeneric<T> Failed(List<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccessful = false;
            return this;
        }
        public CommandResponseGeneric<T> Failed(string error)
        {
            Errors.Add(error);
            IsSuccessful = false;
            return this;
        }
    }
}
