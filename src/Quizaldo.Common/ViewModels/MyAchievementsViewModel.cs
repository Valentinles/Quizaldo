using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class MyAchievementsViewModel
    {
        public string UserId { get; set; }

        public QuizaldoUser User { get; set; }

        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }

        public DateTime AchievedOn { get; set; }
    }
}
