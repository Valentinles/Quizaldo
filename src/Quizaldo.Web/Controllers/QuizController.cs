using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizaldo.Common.ViewModels;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using Quizaldo.Common.ServiceModels;

namespace Quizaldo.Web.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizService quizService;
        private readonly IMapper mapper;

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            this.quizService = quizService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateQuiz()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateQuiz(CreateQuizBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var quiz = mapper.Map<Quiz>(model);

            await this.quizService.CreateQuiz(quiz);

            return RedirectToAction("Index", "Home"); 
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            await this.quizService.DeleteQuiz(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> StartQuiz(int id)
        {
            var getQuiz = await this.quizService.GetQuizById(id);

            var quiz = mapper.Map<QuizViewModel>(getQuiz);

            if (quiz == null)
            {
                return NotFound();
            }

            return this.View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> StartQuiz(QuizViewModel model)
        {
            var quiz = mapper.Map<QuizServiceModel>(model);

            await this.quizService.StartQuiz(quiz, this.User.Identity.Name);

            return RedirectToAction("Result", "UserResult", new { id=quiz.Result.Id });
        }

        public async Task<IActionResult> SearchResults(string searchTerm)
        {
            if (searchTerm == null)
            {
                return NotFound();
            }

            var foundQuizzes = await this.quizService.GetSearchingResults(searchTerm);

            var result = new SearchResultsViewModel();
            result.SearchTerm = searchTerm;
            result.Quizzes = foundQuizzes;

            return this.View(result);
        }

    }
}