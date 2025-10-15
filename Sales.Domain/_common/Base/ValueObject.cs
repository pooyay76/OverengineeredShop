namespace Sales.Domain._common.Base
{
    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as ValueObject;

            if (ReferenceEquals(valueObject, null))
                return false;

            return EqualsBase(valueObject);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract bool EqualsBase(ValueObject other);
        protected virtual int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
