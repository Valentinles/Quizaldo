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

namespace Quizaldo.Web.Controllers
{
    public class JokeController : Controller
    {
        private readonly IJokeService jokeService;
        private readonly IMapper mapper;

        public JokeController(IJokeService jokeService, IMapper mapper)
        {
            this.jokeService = jokeService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddJoke()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddJoke(AddJokeBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var joke = mapper.Map<Joke>(model);

            await this.jokeService.AddJoke(joke);

            return RedirectToAction("AllJokes", "Joke");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteJoke(int id)
        {
            await this.jokeService.DeleteJoke(id);

            return RedirectToAction("AllJokes", "Joke");
        }

        public async Task<IActionResult> UpvoteJoke(int id)
        {
            await this.jokeService.UpvoteJoke(id, this.User.Identity.Name);

            return RedirectToAction("AllJokes", "Joke");
        }

        public async Task<IActionResult> DownvoteJoke(int id)
        {
            await this.jokeService.DownvoteJoke(id, this.User.Identity.Name);

            return RedirectToAction("AllJokes", "Joke");
        }

        public async Task<IActionResult> AllJokes()
        {
            var jokes = (await this.jokeService.GetAllJokes())
                .Select(mapper.Map<AllJokesViewModel>);

            return this.View(jokes);
        }
    }
}
