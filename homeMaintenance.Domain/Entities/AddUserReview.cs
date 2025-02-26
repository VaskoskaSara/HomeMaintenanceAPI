﻿namespace homeMaintenance.Domain.Entities
{
    public class AddUserReview
    {
        public Guid PaymentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid UserId { get; set; }
        public int? Rating { get; set; }
        public string[]? Photos { get; set; }
        public string? Comment { get; set; }
    }
}
