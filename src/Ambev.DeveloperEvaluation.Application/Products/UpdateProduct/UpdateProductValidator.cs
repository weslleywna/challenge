using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Required
        /// - Name: Required, must be between 0 and 200 characters
        /// - Description: Must be between 0 and 200 characters
        /// - UnitPrice: Must be greater than zero
        /// </remarks>
        public UpdateProductValidator()
        {
            RuleFor(product => product.Id).NotEmpty();
            RuleFor(product => product.Name).NotEmpty().Length(0, 200);
            RuleFor(product => product.Description).Length(0, 200);
            RuleFor(product => product.UnitPrice).GreaterThan(0);
        }
    }
}
