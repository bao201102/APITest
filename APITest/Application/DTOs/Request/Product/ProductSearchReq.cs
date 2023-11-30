using APITest.Application.Configs;
using FluentValidation;
using System.Data.SqlTypes;

namespace APITest.Application.DTOs.Request.Product
{
    public class ProductSearchReq
    {
        public string keysearch { get; set; } = string.Empty;
        public DateTime date_from { get; set; } = DateTime.Now.AddDays(-30);
        public DateTime date_to { get; set; } = DateTime.Now;
        public int page_size { get; set; } = 10;
        public int page_index { get; set; } = 0;
    }

    public class ProductSearchReqValidator : AbstractValidator<ProductSearchReq>
    {
        public ProductSearchReqValidator()
        {
            RuleFor(x => x.page_size).NotNull().GreaterThanOrEqualTo(-1).LessThanOrEqualTo(ModelConfig.PageSizeMaxValue);
            RuleFor(x => x.page_size).GreaterThan(0).When(c => c.page_index >= 0);
            RuleFor(x => x.page_index).NotNull().GreaterThanOrEqualTo(-1);
            RuleFor(x => x.page_index).GreaterThanOrEqualTo(0).When(c => c.page_size > 0);

            RuleFor(x => x.date_from).NotEmpty().NotNull().GreaterThanOrEqualTo((DateTime)SqlDateTime.MinValue).LessThanOrEqualTo(x => x.date_to);
            RuleFor(x => x.date_to).NotEmpty().NotNull().GreaterThanOrEqualTo(x => x.date_from).LessThanOrEqualTo((DateTime)SqlDateTime.MaxValue);
        }
    }
}
