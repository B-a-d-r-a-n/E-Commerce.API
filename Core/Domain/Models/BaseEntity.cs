

namespace Domain.Models
{
    // parent for all domain models that depend on DbContext
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } //Pk
    }
}
