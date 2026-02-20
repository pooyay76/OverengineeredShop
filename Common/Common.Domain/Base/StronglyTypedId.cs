namespace Common.Domain.Base
{
    public abstract record StronglyTypedId : ValueObject
    {
        private Guid _id;

        public Guid Value
        {
            get { return _id; }
            private set
            {
                _id = value;
            }
        }


        public  StronglyTypedId(Guid value)
        {
            Value = value;
        }

        public StronglyTypedId()
        {
            Value = Guid.NewGuid();
        }


    }
}
