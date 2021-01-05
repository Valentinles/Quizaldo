using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.Constants;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Implementations
{
    public class AchievementService : DataService, IAchievementService
    {
        private readonly INotificationService notificationService;
        public AchievementService(QuizaldoDbContext context, INotificationService notificationService) : base(context)
        {
            this.notificationService = notificationService;
        }

        public async Task<IEnumerable<Achievement>> GetAllAchievements()
        {
            return await this.context.Achievements.ToListAsync();
        }

        public async Task<IEnumerable<UserAchievement>> GetAchievementsByUser(string username)
        {
            return await this.context.UserAchievements
                .Where(x => x.User.UserName.Equals(username))
                .Include(x=>x.Achievement)
                .OrderByDescending(x=>x.AchievedOn)
                .ToListAsync();
        }

        public async Task GetRookieAchievement(QuizaldoUser user)
        {
            var achievement = await this.context.Achievements
                .FirstOrDefaultAsync(x => x.Name.Equals(AchievementConstants.Rookie));

            if (achievement == null)
            {
                return;
            }

            if (!this.context.UserAchievements.Any(x => x.UserId == user.Id && x.AchievementId == achievement.Id))
            {
                var userAchivement = new UserAchievement();
                userAchivement.UserId = user.Id;
                userAchivement.User = user;
                userAchivement.AchievementId = achievement.Id;
                userAchivement.Achievement = achievement;
                userAchivement.AchievedOn = DateTime.Now;

                user.TotalAchievementPoints += achievement.Points;

                await this.context.UserAchievements.AddAsync(userAchivement);
                await this.context.SaveChangesAsync();
                await this.notificationService.CreateAchievementNotification(user, achievement);
            }
        }

        public async Task GetHundrederAchievement(QuizaldoUser user)
        {
            var achievement = await this.context.Achievements
                .FirstOrDefaultAsync(x => x.Name.Equals(AchievementConstants.Hundreder));
            if (achievement == null)
            {
                return;
            }

            if (user.TotalQuizPoints >= 100)
            {
                if (!this.context.UserAchievements.Any(x => x.UserId == user.Id && x.AchievementId == achievement.Id))
                {
                    var userAchivement = new UserAchievement();
                    userAchivement.UserId = user.Id;
                    userAchivement.User = user;
                    userAchivement.AchievementId = achievement.Id;
                    userAchivement.Achievement = achievement;
                    userAchivement.AchievedOn = DateTime.Now;

                    user.TotalAchievementPoints += achievement.Points;

                    await this.context.UserAchievements.AddAsync(userAchivement);
                    await this.context.SaveChangesAsync();
                    await this.notificationService.CreateAchievementNotification(user, achievement);
                }
            }
        }

        public async Task GetExcellentAchievement(QuizaldoUser user)
        {
            var achievement = await this.context.Achievements
                .FirstOrDefaultAsync(x => x.Name.Equals(AchievementConstants.Excellent));
            if (achievement == null)
            {
                return;
            }

            var userResults = await this.context.UserResults
                .Where(x=>x.User.Id==user.Id)
                .ToListAsync();


            if (userResults.Any(x=>x.UsersWrongAnswers == 0))
            {
                if (!this.context.UserAchievements.Any(x => x.UserId == user.Id && x.AchievementId == achievement.Id))
                {
                    var userAchivement = new UserAchievement();
                    userAchivement.UserId = user.Id;
                    userAchivement.User = user;
                    userAchivement.AchievementId = achievement.Id;
                    userAchivement.Achievement = achievement;
                    userAchivement.AchievedOn = DateTime.Now;

                    user.TotalAchievementPoints += achievement.Points;

                    await this.context.UserAchievements.AddAsync(userAchivement);
                    await this.context.SaveChangesAsync();
                    await this.notificationService.CreateAchievementNotification(user, achievement);
                }
            }
        }

        public async Task GetMasterAchievement(QuizaldoUser user)
        {
            var achievement = await this.context.Achievements
                .FirstOrDefaultAsync(x => x.Name.Equals(AchievementConstants.Master));
            if (achievement == null)
            {
                return;
            }

            var userAchievements = await this.context.UserAchievements
                .Where(x => x.User.Id == user.Id)
                .Include(x=>x.Achievement)
                .ToListAsync();

            //master achievement requires all previous achievements to be completed
            var allAchievementsCount = this.context.Achievements.Count() - 1;

            if (userAchievements.Count() == allAchievementsCount)
            {
                if (!this.context.UserAchievements.Any(x => x.UserId == user.Id && x.AchievementId == achievement.Id))
                {
                    var userAchivement = new UserAchievement();
                    userAchivement.UserId = user.Id;
                    userAchivement.User = user;
                    userAchivement.AchievementId = achievement.Id;
                    userAchivement.Achievement = achievement;
                    userAchivement.AchievedOn = DateTime.Now;

                    user.TotalAchievementPoints += achievement.Points;

                    await this.context.UserAchievements.AddAsync(userAchivement);
                    await this.context.SaveChangesAsync();
                    await this.notificationService.CreateAchievementNotification(user, achievement);
                }
            }
        }
    }
}
