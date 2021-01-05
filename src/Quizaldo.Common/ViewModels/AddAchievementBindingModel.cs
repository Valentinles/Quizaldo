using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class AddAchievementBindingModel
    {
        [Required(ErrorMessage = "Name is required and must be maximum 50 symbols!")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Requirement is required and must be maximum 100 symbols!")]
        [StringLength(100)]
        public string Requirement { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        [Range(1,1000, ErrorMessage = "Must be between 1 and 1000 points!")]
        public int Points { get; set; }
    }
}
