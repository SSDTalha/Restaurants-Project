namespace Restaurants.Domain.Entities;

public class AuditableTenantEntity
{

    public string? CompanyId { get; set; }

    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
