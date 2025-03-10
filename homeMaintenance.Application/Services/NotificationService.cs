﻿using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;

namespace homeMaintenance.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IList<NotificationDto>> GetUnsendNotifications(Guid userId)
        {
            var unsentNotifications = await _notificationRepository.GetUnsentNotifications(userId);

            foreach (var notification in unsentNotifications)
            {
                await _notificationRepository.MarkNotificationAsSent(notification.Id);

            }

            return unsentNotifications.Select(notification => new NotificationDto
            {
                EmployeeId = notification.EmployeeId,
                UserPaymentId = notification.UserPaymentId,
                StartDate = notification.StartDateTime,
                EndDate = notification.EndDateTime
            }).ToList();
        }
    }
}