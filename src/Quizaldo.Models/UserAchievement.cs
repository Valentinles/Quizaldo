using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Models
{
    public class UserAchievement
    {
        public string UserId { get; set; }

        public QuizaldoUser User { get; set; }

        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }

        public DateTime AchievedOn { get; set; }
    }
}
