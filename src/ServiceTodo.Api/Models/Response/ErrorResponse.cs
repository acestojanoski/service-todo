namespace ServiceTodo.Api.Models.Response
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; } = 500;
        public string Message { get; set; } = "Internal server error";
    }
}
