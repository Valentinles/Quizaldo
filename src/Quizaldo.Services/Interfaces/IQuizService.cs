using Quizaldo.Common.ViewModels;
using Quizaldo.Models;
using Quizaldo.Services.Implementations;
using Quizaldo.Common.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IQuizService
    {
        Task CreateQuiz(Quiz quiz);

        Task DeleteQuiz(int id);

        Task<IEnumerable<Quiz>> AllQuizzes();

        Task<Quiz> GetQuizById(int id);

        Task StartQuiz(QuizServiceModel model, string username);

        Task<List<Quiz>> GetSearchingResults(string searchTerm);
    }
}
