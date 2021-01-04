using AutoMapper;
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
    public class QuestionSuggestionService : DataService, IQuestionSuggestionService
    {
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;
        public QuestionSuggestionService(QuizaldoDbContext context, IMapper mapper, INotificationService notificationService) : base(context)
        {
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        public async Task SuggestQuestion(QuestionSuggestion questionSuggestion, string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return;
            }

            questionSuggestion.User = user;
            questionSuggestion.SuggestedOn = DateTime.Now;

            if (questionSuggestion.CorrectAnswer == questionSuggestion.FirstOption ||
                questionSuggestion.CorrectAnswer == questionSuggestion.SecondOption ||
                questionSuggestion.CorrectAnswer == questionSuggestion.ThirdOption ||
                questionSuggestion.CorrectAnswer == questionSuggestion.FourthOption)
            {
                await this.context.QuestionSuggestions.AddAsync(questionSuggestion);
            }
            else
            {
                return;
            }

            await this.notificationService.CreateQuestionSuggestionNotification(user, questionSuggestion);

            await this.context.SaveChangesAsync();
        }

        public async Task ApproveSuggestion(int questionSuggestionId)
        {
            var suggestedQuestion = await this.context.QuestionSuggestions.FirstOrDefaultAsync(q => q.Id == questionSuggestionId);

            if (suggestedQuestion == null)
            {
                return;
            }

            var mappedApprovedQuestion = mapper.Map<Question>(suggestedQuestion);

            await this.context.Questions.AddAsync(mappedApprovedQuestion);
            this.context.QuestionSuggestions.Remove(suggestedQuestion);

            await this.context.SaveChangesAsync();
        }

        public async Task RemoveSuggestion(int questionSuggestionId)
        {
            var suggestedQuestion = await this.context.QuestionSuggestions.FirstOrDefaultAsync(q => q.Id == questionSuggestionId);

            if (suggestedQuestion == null)
            {
                return;
            }

            this.context.QuestionSuggestions.Remove(suggestedQuestion);

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionSuggestion>> GetAllSuggestions()
        {
            var allSuggestions = await this.context.QuestionSuggestions.
                Include(u=>u.User)
                .Include(q=>q.Quiz)
                .OrderBy(s=>s.SuggestedOn)
                .ToListAsync();

            return allSuggestions;
        }
    }
}
