using FluentValidation;

using Notes.API.Models.DTO;

namespace Notes.API.Validators;

public class LoginRequestValidator: AbstractValidator<Models.DTO.LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
