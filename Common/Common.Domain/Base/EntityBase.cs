namespace Common.Domain.Base
{
    public abstract class EntityBase<TKey> where TKey:StronglyTypedId,new()
    {

        public TKey Id { get; protected set; } = new();


        public override bool Equals(object obj)
        {
            var entity = obj as EntityBase<TKey>;
            return entity != null &&
                GetType() == entity.GetType() &&
                EqualityComparer<TKey>.Default.Equals(Id, entity.Id);
        }

        public static bool operator ==(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }
    }
}
