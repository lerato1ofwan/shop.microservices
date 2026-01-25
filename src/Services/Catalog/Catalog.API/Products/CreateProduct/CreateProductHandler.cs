namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("Product image is required.");
        }
    }

    internal class CreateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create product entity (command -> entity)
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // Add product entity to the database and save
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return the create product result
            return new CreateProductResult(product.Id);
        }
    }
}