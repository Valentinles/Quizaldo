using Microsoft.EntityFrameworkCore;
using Quizaldo.Data;
using Quizaldo.Models;
using Quizaldo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Implementations
{
    public class JokeService : DataService, IJokeService
    {
        public JokeService(QuizaldoDbContext context) : base(context)
        {
        }

        public async Task AddJoke(Joke joke)
        {
            await this.context.Jokes.AddAsync(joke);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteJoke(int id)
        {
            var joke = await this.context.Jokes.FirstOrDefaultAsync(j => j.Id == id);

            if (joke == null)
            {
                return;
            }

            this.context.Jokes.Remove(joke);
            await this.context.SaveChangesAsync();
        }

        public async Task UpvoteJoke(int id, string username)
        {
            var joke = await this.context.Jokes.FirstOrDefaultAsync(j => j.Id == id);
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));

            if (joke == null || user == null)
            {
                return;
            }
            if (user.VotedJokes.Contains(joke))
            {
                return;
            }
            else
            {
                joke.PositiveVotes++;
                user.VotedJokes.Add(joke);
            }
            

            this.context.Jokes.Update(joke);
            await this.context.SaveChangesAsync();
        }
        public async Task DownvoteJoke(int id, string username)
        {
            var joke = await this.context.Jokes.FirstOrDefaultAsync(j => j.Id == id);
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));

            if (joke == null || user == null)
            {
                return;
            }

            if (user.VotedJokes.Contains(joke))
            {
                return;
            }
            else
            {
                joke.NegativeVotes++;
                user.VotedJokes.Add(joke);
            }

            this.context.Jokes.Update(joke);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Joke>> GetAllJokes()
        {
            return await this.context.Jokes
                .OrderByDescending(j => j.PositiveVotes)
                .ThenBy(j=>j.NegativeVotes)
                .ToListAsync();
        }
    }
}
