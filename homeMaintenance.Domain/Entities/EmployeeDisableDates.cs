namespace homeMaintenance.Domain.Entities
{
    public class EmployeeDisableDates
    {
        public Guid UserId { get; set; }
        public DateTime[]? DisabledDates { get; set; }
        public bool? IsEnabled { get; set; }

    }
}
