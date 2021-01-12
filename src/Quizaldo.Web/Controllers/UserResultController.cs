using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizaldo.Common.ViewModels;
using Quizaldo.Services.Interfaces;

namespace Quizaldo.Web.Controllers
{
    [Authorize]
    public class UserResultController : Controller
    {
        private readonly IUserResultService userResultService;
        private readonly IMapper mapper;

        public UserResultController(IUserResultService userResultService, IMapper mapper)
        {
            this.userResultService = userResultService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Result(int id)
        {
            var getUserResult = await this.userResultService.GetUserResultById(id, this.User.Identity.Name);

            var userResult = mapper.Map<UserResultViewModel>(getUserResult);

            try
            {
                if (userResult.User == null)
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                
                return NotFound();
            }

            return View(userResult);
        }

        public async Task<IActionResult> MyResults(string username)
        {
            var userResults = (await this.userResultService.GetAllUserResultsByUser(this.User.Identity.Name))
                .Select(mapper.Map<UserResultViewModel>);

            return this.View(userResults);
        }
    }
}