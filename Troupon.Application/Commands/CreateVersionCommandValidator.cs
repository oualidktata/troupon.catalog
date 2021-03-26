using FluentValidation;

namespace Troupon.Catalog.Core.Application.Commands
{
    class CreateDealCommandValidator:AbstractValidator<CreateDealCommand>
    {
        public CreateDealCommandValidator()
        {
            RuleFor(c => c.Major).NotEmpty();
            RuleFor(c => c.Minor).NotEmpty();
            RuleFor(c => c.Patch).NotEmpty();
        }
    }
}
