using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRead { get; set; } = false;


        public string UserId { get; set; }

        public QuizaldoUser User { get; set; }
    }
}
