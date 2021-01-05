using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class UsersRanklistViewModel
    {
        public string Username { get; set; }

        public int TotalQuizPoints { get; set; }

        public int TotalAchievementPoints { get; set; }
    }
}
