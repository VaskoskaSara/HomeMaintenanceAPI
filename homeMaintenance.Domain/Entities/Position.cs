namespace homeMaintenance.Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; }
        public required string PositionName { get; set; }    
    }
}
