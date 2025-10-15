namespace Sales.Domain._common.Base
{
    public abstract class StronglyTypedId : ValueObject
    {
        private Guid _id;

        public Guid Value
        {
            get { return _id; }
            private set
            {
                if (value == Guid.Empty)
                    throw new DomainException("");

                _id = value;
            }
        }

        protected StronglyTypedId(Guid value)
        {
            Value = value;
        }

        protected override bool EqualsBase(ValueObject other)
        {

            return Value == (other as StronglyTypedId).Value;
        }
        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
