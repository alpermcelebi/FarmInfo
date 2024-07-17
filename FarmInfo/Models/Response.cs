namespace FarmInfo.Models
{
    public class ServiceResponse<T>
    {
        public T? Value { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
