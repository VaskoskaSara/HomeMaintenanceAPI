namespace homeMaintenance.Application.DTOs
{
    public class UserReviewsDto
    {
        public Guid UserId { get; set; }                
        public string FullName { get; set; }            
        public string? Avatar { get; set; }            
        public string? Comment { get; set; }           
        public int Rating { get; set; }             
        public List<string>? Photos { get; set; } = new List<string>(); 
        public string? UserPaymentId { get; set; }
    }
}
