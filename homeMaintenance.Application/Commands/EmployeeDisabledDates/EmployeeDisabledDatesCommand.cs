using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public record EmployeeDisabledDates : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public DateTime?[] DisabledDates { get; set; }
        public bool? IsEnabled { get; set; }

    }
}
