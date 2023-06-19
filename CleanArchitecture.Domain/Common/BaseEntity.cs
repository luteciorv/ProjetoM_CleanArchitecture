namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }

        public DateTimeOffset DateCreated { get; }
        public DateTimeOffset? DateUpdated { get; protected set;}
        public DateTimeOffset? DateDeleted { get; protected set;}
       
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            
            DateCreated = DateTime.UtcNow;
            DateUpdated = null;
            DateDeleted = null;
        }
    }
}
