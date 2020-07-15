using FluentValidation;
using ServiceTodo.Api.Models.Request;

namespace ServiceTodo.Api.Models.Validation
{
    public class GetTodosRequestValidator : AbstractValidator<GetTodosRequest>
    {
        public GetTodosRequestValidator()
        {
            RuleFor(x => x.PageCount).GreaterThan(0);
            RuleFor(x => x.PageNumber).GreaterThan(0);
        }
    }
}
