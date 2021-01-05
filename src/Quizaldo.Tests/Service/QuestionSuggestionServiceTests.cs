using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.Mapper;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Implementations;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Quizaldo.Tests.Service
{
    public class QuestionSuggestionServiceTests
    {
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;

        [Theory]
        [InlineData("testUser")]
        public async Task SuggestQuestion_WithCorrectInputDataAndUser_ShouldReturnSameQuestion(string username)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            var question = GetTestQuestionSuggestion();

            SeedTestUser(context);

            await questionSuggestionService.SuggestQuestion(question, username);

            Assert.Equal(1, context.QuestionSuggestions.Count());
        }

        [Theory]
        [InlineData("wrongTestUser")]
        public async Task SuggestQuestion_WithCorrectInputDataAndIncorretUser_ShouldntReturnQuestion(string username)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            var question = GetTestQuestionSuggestion();

            SeedTestUser(context);

            await questionSuggestionService.SuggestQuestion(question, username);

            Assert.Equal(0, context.QuestionSuggestions.Count());
        }

        [Theory]
        [InlineData("testUserk")]
        public async Task SuggestQuestion_WithIncorrectInputDataAndCorrectUser_ShouldntReturnQuestion(string username)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            var question = GetTestQuestionSuggestion();

            question.CorrectAnswer = "wrong";

            SeedTestUser(context);

            await questionSuggestionService.SuggestQuestion(question, username);

            Assert.Equal(0, context.QuestionSuggestions.Count());
        }

        [Theory]
        [InlineData(7)]
        public async Task ApproveSuggestion_WithCorrectData_ShouldAddQuestion(int questionSuggestionId)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Question, QuestionSuggestion>().ReverseMap();
            });

            var mapper = config.CreateMapper();

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            SeedTestQuestionSuggestion(context);

            await questionSuggestionService.ApproveSuggestion(questionSuggestionId);

            Assert.Equal(0, context.QuestionSuggestions.Count());
        }

        [Theory]
        [InlineData(1)]
        public async Task ApproveSuggestion_WithIncorrectData_ShouldntAddQuestion(int questionSuggestionId)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Question, QuestionSuggestion>().ReverseMap();
            });

            var mapper = config.CreateMapper();

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            SeedTestQuestionSuggestion(context);

            await questionSuggestionService.ApproveSuggestion(questionSuggestionId);

            Assert.Equal(0, context.Questions.Count());
        }

        [Theory]
        [InlineData(7)]
        public async Task RemoveSuggestion_WithCorrectData_ShouldRemoveSuggestion(int questionSuggestionId)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            SeedTestQuestionSuggestion(context);

            await questionSuggestionService.RemoveSuggestion(questionSuggestionId);

            Assert.Equal(0, context.QuestionSuggestions.Count());
        }

        [Theory]
        [InlineData(1)]
        public async Task RemoveSuggestion_WithIncorrectData_ShouldntRemoveSuggestion(int questionSuggestionId)
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            SeedTestQuestionSuggestion(context);

            await questionSuggestionService.RemoveSuggestion(questionSuggestionId);

            Assert.Equal(1, context.QuestionSuggestions.Count());
        }

        [Fact]
        public async Task GetAllSuggestions_WithData_ShouldReturnAllSuggestions()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            SeedTestQuestionSuggestion(context);

            var result = await questionSuggestionService.GetAllSuggestions();

            Assert.False(result.Count() == 0);
        }

        [Fact]
        public async Task GetAllSuggestions_WithNoData_ShouldReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionSuggestionService = new QuestionSuggestionService(context, mapper, notificationService);

            var result = await questionSuggestionService.GetAllSuggestions();

            Assert.True(result.Count() == 0);
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

        private void SeedTestQuestionSuggestion(QuizaldoDbContext context)
        {
            context.QuestionSuggestions.Add(GetTestQuestionSuggestion());
            context.SaveChanges();
        }

        private QuestionSuggestion GetTestQuestionSuggestion()
        {
           var user = GetTestUser();

            return new QuestionSuggestion()
            {
                Id = 7,
                QuestionName = "Is it a correct test?",
                FirstOption = "Yes",
                SecondOption = "No",
                ThirdOption = "Idk",
                FourthOption = "Maybe",
                CorrectAnswer = "Yes",
                Quiz = new Quiz(),
                SuggestedOn = DateTime.Now,
                User = user
            };
        }
    }
}
