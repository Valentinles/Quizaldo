using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quizaldo.Common.ViewModels;
using Quizaldo.Services.Interfaces;

namespace Quizaldo.Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            this.notificationService = notificationService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> RecentNotifications()
        {
            var notifications = (await this.notificationService.GetRecentNotifications())
                .Select(mapper.Map<NotificationListingViewModel>);

            return this.View(notifications);
        }

        public async Task<IActionResult> AllNotifications()
        {
            var notifications = (await this.notificationService.GetAllNotifications())
                .Select(mapper.Map<NotificationListingViewModel>);

            return this.View(notifications);
        }
    }
}
