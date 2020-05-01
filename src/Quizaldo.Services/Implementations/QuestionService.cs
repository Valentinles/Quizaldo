using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Common.ViewModels;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quizaldo.Services.Implementations
{
    public class QuestionService : DataService, IQuestionService
    {
        public QuestionService(QuizaldoDbContext context) : base(context)
        {
        }

        public async Task AddQuestion(Question question)
        {
            if (question.CorrectAnswer == question.FirstOption || question.CorrectAnswer == question.SecondOption ||
                question.CorrectAnswer == question.ThirdOption || question.CorrectAnswer == question.FourthOption)
            {
                await this.context.Questions.AddAsync(question);
            }
            else
            {
                return;
            }

            await this.context.SaveChangesAsync();
        }
    }
}
