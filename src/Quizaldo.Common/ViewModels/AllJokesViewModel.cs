using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Common.ViewModels
{
    public class AllJokesViewModel
    {
        public int Id { get; set; }

        public string JokeContent { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }
    }
}
