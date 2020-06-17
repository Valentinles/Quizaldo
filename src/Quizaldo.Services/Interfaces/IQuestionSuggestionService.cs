using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IQuestionSuggestionService
    {
        Task SuggestQuestion(QuestionSuggestion questionSuggestion, string username);

        Task ApproveSuggestion(int questionSuggestionId);

        Task RemoveSuggestion(int questionSuggestionId);

        Task<IEnumerable<QuestionSuggestion>> GetAllSuggestions();
    }
}
