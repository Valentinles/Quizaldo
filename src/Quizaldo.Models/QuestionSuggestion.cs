using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Models
{
    public class QuestionSuggestion
    {
        public int Id { get; set; }

        public string QuestionName { get; set; }

        public string FirstOption { get; set; }

        public string SecondOption { get; set; }

        public string ThirdOption { get; set; }

        public string FourthOption { get; set; }

        public string CorrectAnswer { get; set; }

        public int CorrectAnswerPoints { get; set; }

        public DateTime SuggestedOn { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public string UserId { get; set; }

        public QuizaldoUser User { get; set; }
    }
}
