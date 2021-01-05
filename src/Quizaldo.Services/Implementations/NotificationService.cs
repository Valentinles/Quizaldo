using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.ServiceModels;
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
    public class NotificationService : DataService, INotificationService
    {
        public NotificationService(QuizaldoDbContext context) : base(context)
        {
        }

        public async Task CreateQuestionSuggestionNotification(QuizaldoUser user, QuestionSuggestion questionSuggestion)
        {
            var notification = new Notification();
            notification.Text = $"{user.UserName} has suggested a question:\n " +
                $"{questionSuggestion.QuestionName} with correct answer:\n " +
                $"{questionSuggestion.CorrectAnswer}.";
            notification.CreatedOn = DateTime.Now;
            notification.UserId = user.Id;
            notification.User = user;

            await this.context.Notifications.AddAsync(notification);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateQuizNotification(Quiz quiz, QuizaldoUser user, QuizServiceModel model)
        {
            var notification = new Notification();
            notification.Text = $"{user.UserName} has completed {quiz.Name} quiz with " +
                $"{model.Result.UsersCorrectAnswers} correct and " +
                $"{model.Result.UsersWrongAnswers} wrong answers.";
            notification.CreatedOn = DateTime.Now;
            notification.UserId = user.Id;
            notification.User = user;

            await this.context.Notifications.AddAsync(notification);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateAchievementNotification(QuizaldoUser user, Achievement achievement)
        {
            var notification = new Notification();
            notification.Text = $"{user.UserName} has completed {achievement.Name} achievement ";
            notification.CreatedOn = DateTime.Now;
            notification.UserId = user.Id;
            notification.User = user;

            await this.context.Notifications.AddAsync(notification);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetRecentNotifications()
        {
            return await this.context.Notifications
                .Where(x => x.CreatedOn.Date == DateTime.Today)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await this.context.Notifications
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }
    }
}
