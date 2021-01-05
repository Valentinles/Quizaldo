using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.ServiceModels;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Implementations;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Quizaldo.Tests.Service
{
    public class QuizServiceTests
    {
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;
        private readonly IAchievementService achievementService;

        [Fact]
        public async Task CreateQuiz_WithCorrectInputData_ShouldCreateQuiz()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            var quiz = GetTestQuiz();

            await quizService.CreateQuiz(quiz);

            Assert.True(quiz.Id > 0);
        }

        [Fact]
        public async Task Create_WithIncorrectData_ShouldntCreateQuiz()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            var quiz = GetTestQuiz();
            quiz.Id = 0;

            await quizService.CreateQuiz(quiz);

            Assert.Equal(0, context.Questions.Count());
        }

        [Theory]
        [InlineData(7)]
        public async Task DeleteQuiz_WithCorrectData_ShouldDeleteQuiz(int id)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            SeedTestQuiz(context);

            await quizService.DeleteQuiz(id);

            Assert.Equal(0, context.Quizzes.Count());
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteQuiz_WithIncorrectData_ShouldntDeleteQuiz(int id)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            SeedTestQuiz(context);

            await quizService.DeleteQuiz(id);

            Assert.Equal(1, context.Quizzes.Count());
        }

        [Fact]
        public async Task AllQuizzes_WithData_ShouldReturnAllQuizzes()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            SeedTestQuiz(context);

            var result = await quizService.AllQuizzes();

            Assert.False(result.Count() == 0);
        }

        [Fact]
        public async Task AllQuizzes_WithNoData_ShouldReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            var result = await quizService.AllQuizzes();

            Assert.True(result.Count() == 0);
        }

        [Theory]
        [InlineData(7)]
        public async Task GetQuizById_WithCorrectData_ShouldReturnQuiz(int id)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var quizService = new QuizService(context, mapper, notificationService, achievementService);

            SeedTestQuiz(context);

            var currentQuiz = await quizService.GetQuizById(id);

            Assert.True(currentQuiz.Id == id);
        }

        private QuizaldoUser GetTestUser()
        {
            return new QuizaldoUser()
            {
                UserName = "testUser"
            };
        }

        private void SeedTestUser(QuizaldoDbContext context)
        {
            context.Users.Add(GetTestUser());
            context.SaveChanges();
        }

        private Quiz GetTestQuiz()
        {
            var quizQuestion = GetTestQuestion();

            return new Quiz()
            {
                Id = 7,
                Name = "TestQuiz",
                QuizQuestions = new List<Question>()
                {
                   quizQuestion
                }
            };
        }

        private void SeedTestQuiz(QuizaldoDbContext context)
        {
            context.Quizzes.Add(GetTestQuiz());
            context.SaveChanges();
        }

        private Question GetTestQuestion()
        {
            var quiz = GetTestQuiz();

            return new Question()
            {
                Id = 7,
                QuestionName = "Is it a correct test?",
                FirstOption = "Yes",
                SecondOption = "No",
                ThirdOption = "Idk",
                FourthOption = "Maybe",
                CorrectAnswer = "Yes",
                Quiz = quiz,
                QuizId = quiz.Id
            };
        }
    }
}
