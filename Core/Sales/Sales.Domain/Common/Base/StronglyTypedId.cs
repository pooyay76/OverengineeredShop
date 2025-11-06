namespace Sales.Domain.Common.Base
{
    public abstract record StronglyTypedId : ValueObject
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


    }
}
