#nullable enable
using Sales.Domain.BillAgg.Exceptions;
using Sales.Domain.Common;
using Sales.Domain.Common.Base;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.DiscountAgg.Models;

namespace Sales.Domain.BillAgg.Models
{
    public class BillItem : Entity<BillItemId>
    {
        public BillId BillId { get; private set; } = null!;
        public Bill Bill { get; private set; } = null!;

        public ProductItemId ProductItemId { get; private set; }
        public int Quantity { get; private set; }

        public Money UnitPriceBase { get; private set; }
        public Money DiscountedAmount { get; private set; }
        public Money UnitPriceWithDiscount { get; private set; }

        public DiscountId? DiscountId { get; private set; }
        public Discount? Discount { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        private BillItem()
        {

        }
        internal BillItem(ProductItemId productId, int quantity, Money unitPriceBase, Discount? discount)
        {
            ProductItemId = productId;
            Quantity = quantity;
            UnitPriceBase = unitPriceBase;

            if (discount == null)
            {
                DiscountId = null;
                DiscountPercentage = 0;
            }
            else
            {
                if (discount.DiscountType != DiscountType.ProductDiscount)
                {
                    throw new BillDiscountsCannotBeSetOnProducts();
                }
                DiscountId = discount.Id;
                DiscountPercentage = discount.DiscountPercentage;

            }

            DiscountedAmount = new Money(UnitPriceBase.GetValue() * (DiscountPercentage / 100));
            UnitPriceWithDiscount = UnitPriceBase - DiscountedAmount;
        }





    }
}
