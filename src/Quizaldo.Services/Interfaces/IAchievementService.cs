using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IAchievementService
    {
        Task<IEnumerable<Achievement>> GetAllAchievements();

        Task<IEnumerable<UserAchievement>> GetAchievementsByUser(string username);

        Task GetRookieAchievement(QuizaldoUser user);

        Task GetHundrederAchievement(QuizaldoUser user);

        Task GetExcellentAchievement(QuizaldoUser user);

        Task GetMasterAchievement(QuizaldoUser user);
    }
}
