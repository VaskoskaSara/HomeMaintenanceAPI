namespace homeMaintenance.Domain.Entities
{
    public class UserReviewsDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Comment { get; set; }
        public int Ratings { get; set; }
        public List<string>? Photos { get; set; }
    }
}
