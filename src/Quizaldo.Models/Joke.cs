using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Models
{
    public class Joke
    {
        public int Id { get; set; }

        public string JokeContent { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }
    }
}
