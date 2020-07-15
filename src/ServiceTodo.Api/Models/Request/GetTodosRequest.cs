namespace ServiceTodo.Api.Models.Request
{
    public class GetTodosRequest
    {
        public int? PageNumber { get; set; }
        public int? PageCount { get; set; }
    }
}
