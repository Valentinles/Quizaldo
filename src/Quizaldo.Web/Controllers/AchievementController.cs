using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizaldo.Common.ViewModels;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;

namespace Quizaldo.Web.Controllers
{
    public class AchievementController : Controller
    {
        private readonly IAchievementService achievementService;
        private readonly IMapper mapper;
        public AchievementController(IAchievementService achievementService, IMapper mapper)
        {
            this.achievementService = achievementService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> AllAchievements()
        {
            var achievements = (await this.achievementService.GetAllAchievements())
                .Select(mapper.Map<AllAchievementsViewModel>);

            return this.View(achievements);
        }

        [Authorize]
        public async Task<IActionResult> MyAchievements()
        {
            var achievements = (await this.achievementService.GetAchievementsByUser(this.User.Identity.Name))
                .Select(mapper.Map<MyAchievementsViewModel>);

            return this.View(achievements);
        }
    }
}
