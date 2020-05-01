using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Implementations
{
    public class UserResultService : DataService, IUserResultService
    {
        private readonly IMapper mapper;

        public UserResultService(QuizaldoDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<UserResult> GetUserResultById(int id, string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            var userResult = await this.context.UserResults.Include(q => q.Quiz).FirstOrDefaultAsync(ur => ur.Id == id);

            return userResult;
        }
    }
}
