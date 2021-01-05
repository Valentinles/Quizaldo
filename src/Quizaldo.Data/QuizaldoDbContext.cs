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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserId, ua.AchievementId });

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement
                {
                    Id = 1,
                    Name = "Rookie",
                    Requirement = "Complete your first quiz.",
                    LogoUrl = "https://i.pinimg.com/originals/a5/69/36/a56936857e3b5ee42eb7f18ceb50adb4.png",
                    Points = 5
                },
                new Achievement
                {
                    Id = 2,
                    Name = "Hundreder",
                    Requirement = "Get 100 points from quizzes.",
                    LogoUrl = "https://webstockreview.net/images/100-clipart-emoji-10.png",
                    Points = 100
                },
                new Achievement
                {
                    Id = 3,
                    Name = "Excellent",
                    Requirement = "Complete a quiz without any mistakes.",
                    LogoUrl = "https://images.vexels.com/media/users/3/207946/isolated/preview/7c504bb059892ea84bd18a76023ba52b-thumb-up-cartoon-hand-by-vexels.png",
                    Points = 75
                },
                new Achievement
                {
                    Id = 4,
                    Name = "Master",
                    Requirement = "Earn all previous achievements.",
                    LogoUrl = "https://pngimage.net/wp-content/uploads/2018/06/master-ball-png-4.png",
                    Points = 180
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<UserResult> UserResults { get; set; }

        public DbSet<QuestionSuggestion> QuestionSuggestions { get; set; }

        public DbSet<Joke> Jokes { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<UserAchievement> UserAchievements { get; set; }
    }
}
