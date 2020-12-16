using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class AddJokeBindingModel
    {
        [Required(ErrorMessage = "The joke must be maximum 200 symbols!")]
        [StringLength(200)]
        public string JokeContent { get; set; }
    }
}
