using Campaigns.WebAPI.Models;
using FluentValidation;

namespace Campaigns.WebAPI.Ifrastructure.Validations
{
    public class ChangeStateValidation : AbstractValidator<ChangeStateDTO>
    {
        public ChangeStateValidation()
        {
            RuleFor<string>((ChangeStateDTO newState) => newState.NewState)
                .Must((string NewState) =>
                {
                    string[] names = Enum.GetNames(typeof(State));
                    for(int i = 0; i < names.Length; i++)
                    {
                        if (names[i].Equals(NewState)) return true;
                    }
                    return false;
                }).WithMessage("There is not state with this name");
        }
    }
}
