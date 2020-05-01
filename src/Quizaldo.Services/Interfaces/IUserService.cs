using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<QuizaldoUser>> GetUsersByTotalPoints();
    }
}
