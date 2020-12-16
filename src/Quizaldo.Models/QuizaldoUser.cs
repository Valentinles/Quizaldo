﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Quizaldo.Models
{
    // Add profile data for application users by adding properties to the QuizaldoUser class
    public class QuizaldoUser : IdentityUser
    {
        public string Name { get; set; }

        public int TotalQuizPoints { get; set; }

        public List<Joke> VotedJokes { get; set; } = new List<Joke>();
    }
}
