namespace Common.Application.DTOs
{
    public class ApplicationResponse<T> 
    {
        public bool IsSuccessful { get; private set; } = false;
        public List<string> Errors { get; private set; } = [];
        public T Result { get; private set; }
        public ApplicationResponse<T> Succeeded(T result)
        {
            if (Errors.Count == 0)
            {
                Result = result;
                IsSuccessful = true;
            }
            return this;
        }
        public ApplicationResponse<T> Failed(List<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccessful = false;
            return this;
        }
        public ApplicationResponse<T> Failed(string error)
        {
            Errors.Add(error);
            IsSuccessful = false;
            return this;
        }
    }
    public class ApplicationResponse
    {
        public bool IsSuccessful { get; private set; } = false;
        public List<string> Errors { get; private set; } = [];
        public ApplicationResponse Succeeded()
        {
            IsSuccessful = true;
            return this;
        }
        public ApplicationResponse Failed(List<string> errors)
        {
            Errors.AddRange(errors);
            IsSuccessful = false;
            return this;
        }
        public ApplicationResponse Failed(string error)
        {
            Errors.Add(error);
            IsSuccessful = false;
            return this;
        }
    }
}
