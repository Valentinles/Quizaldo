using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IJokeService
    {
        Task AddJoke(Joke joke);

        Task DeleteJoke(int id);

        Task UpvoteJoke(int id, string username);

        Task DownvoteJoke(int id, string username);

        Task<IEnumerable<Joke>> GetAllJokes();
    }
}
