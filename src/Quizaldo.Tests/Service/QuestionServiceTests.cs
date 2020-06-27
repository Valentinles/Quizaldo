using Microsoft.EntityFrameworkCore;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Quizaldo.Tests.Service
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task AddQuestion_WithCorrectInputData_ShouldReturnSameQuestion()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionService = new QuestionService(context);

            var question = new Question
            {
                QuestionName = "Is it a correct test?",
                FirstOption = "Yes",
                SecondOption = "No",
                ThirdOption = "Idk",
                FourthOption = "Maybe",
                CorrectAnswer = "Yes"
            }; 

            await questionService.AddQuestion(question);

            Assert.Equal(1, context.Questions.Count());
        }

        [Fact]
        public async Task AddQuestion_WithIncorrectInputData_ShouldntReturnQuestion()
        {
            var options = new DbContextOptionsBuilder<QuizaldoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new QuizaldoDbContext(options);

            var questionService = new QuestionService(context);

            var question = new Question()
            {
                QuestionName = "Is it a correct test?",
                FirstOption = "Yes",
                SecondOption = "No",
                ThirdOption = "Idk",
                FourthOption = "Maybe",
                CorrectAnswer = "oops"
            };

            await questionService.AddQuestion(question);

            Assert.Equal(0, context.Questions.Count());
        }
    }
}
