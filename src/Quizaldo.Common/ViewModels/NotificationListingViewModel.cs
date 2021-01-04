using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class NotificationListingViewModel
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }


        public string UserId { get; set; }

        public QuizaldoUser User { get; set; }
    }
}
