using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductValidator"/> with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Email: Must be in valid format (using EmailValidator)
        /// - Name: Required, must be between 3 and 200 characters
        /// - Price: Must be greater than zero
        /// </remarks>
        public CreateProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().Length(3, 200);
            RuleFor(product => product.UnitPrice).GreaterThan(0);
        }
    }
}
