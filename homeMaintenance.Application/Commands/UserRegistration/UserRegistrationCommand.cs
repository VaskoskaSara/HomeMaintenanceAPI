using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public record UserRegistrationCommand : IRequest<LoggedUser?>
    {
        public UserType UserType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public float? Experience { get; set; }
        public string? PositionId { get; set; }
        public string? NewPosition { get; set; }
        public PaymentType? PaymentType { get; set; }
        public float? Price { get; set; }
        public IFormFile? Avatar { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string? Description { get; set; }


    }
}
