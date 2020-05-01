using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quizaldo.Common.ViewModels;
using Quizaldo.Services.Interfaces;

namespace Quizaldo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Ranklist()
        {
            var users = (await this.userService.GetUsersByTotalPoints())
                .Select(mapper.Map<UsersRanklistViewModel>);

            return this.View(users);
        }
    }
}