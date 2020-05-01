using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quizaldo.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Question> QuizQuestions { get; set; } = new List<Question>();

        public string QuizLogoUrl { get; set; }

    }
}
