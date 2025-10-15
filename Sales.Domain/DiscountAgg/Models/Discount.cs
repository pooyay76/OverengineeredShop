using Sales.Domain._common;
using Sales.Domain._common.Base;

namespace Sales.Domain.DiscountAgg.Models
{
    public class Discount : AggregateRoot<DiscountId>
    {
        public int DiscountPercentage { get; private set; }
        public string Description { get; private set; }
        public ProductItemId ProductItemId { get; private set; }
        public DiscountTargetType TargetType { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public DateTime StartDate { get; init; } = DateTime.UtcNow;
        public DateTime EndDate { get; private set; }

        public Discount(int discountPercentage, ProductItemId productItemId, string description,
            DiscountTargetType targetType, DiscountType discountType, DateTime endDate)
        {
            DiscountPercentage = discountPercentage;
            ProductItemId = productItemId;
            TargetType = targetType;
            DiscountType = discountType;
            EndDate = endDate.ToUniversalTime();
            if (EndDate <= StartDate)
                throw new ArgumentException();
            Description = description;
        }

        public void Deactivate()
        {
            DateTime utcNow = DateTime.UtcNow;
            if (StartDate <= utcNow && EndDate > utcNow)
            {
                EndDate = utcNow;
            }
        }
    }

    public enum DiscountType
    {
        BillDiscount,
        ProductDiscount
    }


}
