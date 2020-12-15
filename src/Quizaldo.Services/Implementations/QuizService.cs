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

        public QuizService(QuizaldoDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task CreateQuiz(Quiz quiz)
        {
            await this.context.Quizzes.AddAsync(quiz);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteQuiz(int id)
        {
            var quiz = await this.context.Quizzes.Include(q=>q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == id);

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
            var quizzes = await this.context.Quizzes.Include(q=>q.QuizQuestions).ToListAsync();

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
                for (int a = 0; a < model.Answers.Count; a++)
                {
                    if (quiz.QuizQuestions[i].Id == model.Answers[a].QuestionId)
                    {
                        if (model.Answers[a].Answer == quiz.QuizQuestions[i].CorrectAnswer)
                        {
                            model.Result.UsersCorrectAnswers++;

                        }
                        else
                        {
                            model.Result.UsersWrongAnswers++;
                        }
                    }
                    i++;
                }
            }

            model.Result.PointsEarned = model.Result.UsersCorrectAnswers;

            user.TotalQuizPoints += model.Result.PointsEarned;

            await this.context.UserResults.AddAsync(model.Result);
            await this.context.SaveChangesAsync();
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
