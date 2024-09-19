namespace SurveyBasket.Api.Entities
{
    public class AudetableEntity
    {
        //foriegn key for userId
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string? UpdatedById { get; set; }
        public DateTime? UpdateOn { get; set; }

        //navigation property
        public ApplicationUser CreatedBy { get; set; } = default!;
        //nullable
        public ApplicationUser? UpdateBy { get; set; }
    }
}
