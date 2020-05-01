using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quizaldo.Common.ViewModels;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;

namespace Quizaldo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IQuizService quizService;
        private readonly IMapper mapper;

        public QuestionController(IQuestionService questionService, IQuizService quizService, IMapper mapper)
        {
            this.questionService = questionService;
            this.quizService = quizService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            var quizzes = (await this.quizService.AllQuizzes())
                .Select(mapper.Map<AllQuizzesViewModel>);

            ViewData["Quizzes"] = new SelectList(quizzes, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var question = mapper.Map<Question>(model);

            await this.questionService.AddQuestion(question);

            return RedirectToAction("StartQuiz", "Quiz", new { id = model.QuizId });
        }
    }
}