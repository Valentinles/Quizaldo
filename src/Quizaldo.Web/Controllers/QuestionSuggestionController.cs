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
    public class QuestionSuggestionController : Controller
    {
        private readonly IQuestionSuggestionService questionSuggestionService;
        private readonly IQuizService quizService;
        private readonly IMapper mapper;

        public QuestionSuggestionController(IQuestionSuggestionService questionSuggestionService, IQuizService quizService, IMapper mapper)
        {
            this.questionSuggestionService = questionSuggestionService;
            this.quizService = quizService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SuggestQuestion()
        {
            var quizzes = (await this.quizService.AllQuizzes())
                .Select(mapper.Map<AllQuizzesViewModel>);

            ViewData["Quizzes"] = new SelectList(quizzes, "Id", "Name");

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SuggestQuestion(SuggestQuestionBindingModel questionSuggestion, string username)
        {
            var suggestQuestion = mapper.Map<QuestionSuggestion>(questionSuggestion);

            await this.questionSuggestionService.SuggestQuestion(suggestQuestion, this.User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveSuggestedQuestion(int id)
        {
            await this.questionSuggestionService.ApproveSuggestion(id);

            return RedirectToAction("AllSuggestions", "QuestionSuggestion");
        }

        public async Task<IActionResult> RemoveSuggestedQuestion(int id)
        {
            await this.questionSuggestionService.RemoveSuggestion(id);

            return RedirectToAction("AllSuggestions", "QuestionSuggestion");
        }

        public async Task<IActionResult> AllSuggestions()
        {
            var suggestions = (await this.questionSuggestionService.GetAllSuggestions())
                .Select(mapper.Map<AllQuestionSuggestionsViewModel>);

            return this.View(suggestions);
        }

    }
}