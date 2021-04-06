using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.ViewModels;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using Quizaldo.Common.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quizaldo.Services.Implementations
{
    public class QuizService : DataService, IQuizService
    {
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;
        private readonly IAchievementService achievementService;

        public QuizService(QuizaldoDbContext context, IMapper mapper, INotificationService notificationService,
            IAchievementService achievementService) : base(context)
        {
            this.mapper = mapper;
            this.notificationService = notificationService;
            this.achievementService = achievementService;
        }

        public async Task CreateQuiz(Quiz quiz)
        {
            await this.context.Quizzes.AddAsync(quiz);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteQuiz(int id)
        {
            var quiz = await this.context.Quizzes.Include(q => q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return;
            }

            this.context.Quizzes.Remove(quiz);
            this.context.Questions.RemoveRange(quiz.QuizQuestions);
            await this.context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Quiz>> AllQuizzes()
        {
            var quizzes = await this.context.Quizzes.Include(q => q.QuizQuestions).ToListAsync();

            return quizzes;
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var quiz = await this.context.Quizzes.Include(q => q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == id);

            return quiz;
        }

        public async Task StartQuiz(QuizServiceModel model, string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var quiz = await this.context.Quizzes.Include(q => q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == model.Id);

            if (user == null || quiz == null)
            {
                return;
            }

            var results = new UserResult();
            results.UserId = user.Id;
            results.User = user;
            results.QuizId = quiz.Id;
            results.Quiz = quiz;

            model.Result = results;

            for (int i = 0; i < quiz.QuizQuestions.Count; i++)
            {
                var questionId = quiz.QuizQuestions[i].Id;
                var correctAnswer = quiz.QuizQuestions[i].CorrectAnswer;
                var currentAnswer = model.Answers.FirstOrDefault(x => x.QuestionId == questionId).Answer;

                var result = currentAnswer == correctAnswer ? model.Result.UsersCorrectAnswers++ : model.Result.UsersWrongAnswers++;
            }

            model.Result.PointsEarned = model.Result.UsersCorrectAnswers;
            user.TotalQuizPoints += model.Result.PointsEarned;

            await this.context.UserResults.AddAsync(model.Result);
            //if user meets the requirements of any achievement he`ll get the achievement after finishes the quiz
            await this.achievementService.GetRookieAchievement(user);
            await this.achievementService.GetHundrederAchievement(user);
            await this.achievementService.GetExcellentAchievement(user);
            await this.achievementService.GetMasterAchievement(user);

            await this.context.SaveChangesAsync();
            await this.notificationService.CreateQuizNotification(quiz, user, model);
        }

        public async Task<List<Quiz>> GetSearchingResults(string searchTerm)
        {
            return await this.context.Quizzes
                .Where(q => q.Name.Contains(searchTerm))
                .Include(q => q.QuizQuestions)
                .OrderByDescending(q => q.Name)
                .ThenByDescending(q => q.QuizQuestions.Count())
                .ToListAsync();
        }
    }
}
