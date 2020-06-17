using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizaldo.Models;

namespace Quizaldo.Data
{
    public class QuizaldoDbContext : IdentityDbContext<QuizaldoUser>
    {
        public QuizaldoDbContext(DbContextOptions<QuizaldoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<UserResult> UserResults { get; set; }

        public DbSet<QuestionSuggestion> QuestionSuggestions { get; set; }
    }
}
