using Quizaldo.Common.ServiceModels;
using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateQuestionSuggestionNotification(QuizaldoUser user, QuestionSuggestion questionSuggestion);

        Task CreateQuizNotification(Quiz quiz, QuizaldoUser user, QuizServiceModel model);

        Task CreateAchievementNotification(QuizaldoUser user, Achievement achievement);

        Task<IEnumerable<Notification>> GetRecentNotifications();

        Task<IEnumerable<Notification>> GetAllNotifications();
    }
}
