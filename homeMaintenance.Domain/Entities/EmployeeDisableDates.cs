namespace homeMaintenance.Domain.Entities
{
    public class DisabledDatesByEmployee
    {
        public Guid UserId { get; set; }
        public DateTime[]? DisabledDates { get; set; }
        public bool? IsEnabled { get; set; }

    }
}
