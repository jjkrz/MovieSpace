using FluentValidation;

namespace Application.MoviePeople.GetMoviePeople
{
    public class GetMoviePeopleQueryValidator : AbstractValidator<GetMoviePeopleQuery>
    {
        public GetMoviePeopleQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(1000);
        }
    }
}

