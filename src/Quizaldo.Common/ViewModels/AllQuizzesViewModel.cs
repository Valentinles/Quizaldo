using Quizaldo.Models;
using Quizaldo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class AllQuizzesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public string QuizLogoUrl { get; set; }

        public List<Question> QuizQuestions { get; set; }
    }
}
