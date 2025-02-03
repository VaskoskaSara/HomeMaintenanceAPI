using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public record UserRegistrationCommand(
            UserType UserType,
            string FullName,
            string Email,
            string Password,
            string City,
            string Address,
            string BirthDate,
            string PhoneNumber,
            float? Experience,
            string? PositionId,
            string? NewPosition,
            PaymentType? PaymentType,
            float? Price,
            IFormFile? Avatar,
            List<IFormFile>? Photos,
            int? NumberOfEmployees,
            string? Description
        ) : IRequest<LoggedUserDto?>;
}
