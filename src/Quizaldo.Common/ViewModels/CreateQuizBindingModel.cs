using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class CreateQuizBindingModel
    {

        [Required(ErrorMessage = "Name is required and must be maximum 20 symbols!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Logo is required!")]
        [Url]
        public string QuizLogoUrl { get; set; }
    }
}
