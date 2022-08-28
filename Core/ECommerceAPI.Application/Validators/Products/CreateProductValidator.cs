using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products;
public class CreateProductValidator : AbstractValidator<ViewModel_Create_Product> {
	public CreateProductValidator() {
		RuleFor(p => p.Name)
			.NotNull()
			.NotEmpty()
				.WithMessage("Lütfen ürün adını yazınız.")
			.MinimumLength(3)
			.MaximumLength(256)
				.WithMessage("Lütgen ürün adını 3 ile 256 karakter arasında giriniz");

		RuleFor(p => p.Stock)
			.NotNull()
			.NotEmpty()
				.WithMessage("Lütfen stok bilgisini giriniz")
			.Must(x => x >= 0)
				.WithMessage("Stock bilgisi 0 veya 0'dan büyük olmalıdır");

		RuleFor(p => p.Price)
			.NotNull()
			.NotEmpty()
				.WithMessage("Lütfen fiyat bilgisini giriniz")
			.Must(x => x > 0)
				.WithMessage("Fiyat bilgisi 0'dan büyük olmalıdır");

	}
}