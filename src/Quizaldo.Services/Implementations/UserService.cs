using Microsoft.EntityFrameworkCore;
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
    public class UserService : DataService, IUserService
    {
        public UserService(QuizaldoDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QuizaldoUser>> GetUsersByTotalPoints()
        {
            var users = await this.context.Users
                .OrderByDescending(u => u.TotalQuizPoints)
                .ThenByDescending(u=>u.TotalAchievementPoints)
                .ToListAsync();

            return users;
        }
    }
}
