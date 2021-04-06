using Quizaldo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quizaldo.Services.Interfaces
{
    public interface IUserResultService
    {
        Task<UserResult> GetUserResultById(int id, string username);

        Task<IEnumerable<UserResult>> GetAllUserResultsByUser(string username);
    }
}
