namespace SurveyBasket.Api.Contracts.Questions
{
    public class QuestionsValidator : AbstractValidator<QuestionsRequest>
    {
        public QuestionsValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .Length(3,1000);


            RuleFor(x => x.Answers)
               .NotNull();

            RuleFor(x => x.Answers)

                .Must(x => x.Count > 1)
                .WithMessage("Questions at list should have 2 answer")
                .When(x => x.Answers != null);

            RuleFor(x => x.Answers)
               
               .Must(x => x.Distinct().Count() ==x.Count)
               .WithMessage("you cant add duplicated answers for the same question")
               .When(x => x.Answers != null);
        }
    }
}
