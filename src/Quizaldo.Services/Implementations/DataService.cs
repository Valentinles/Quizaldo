using Quizaldo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quizaldo.Services.Implementations
{
    public abstract class DataService
    {
        protected readonly QuizaldoDbContext context;

        public DataService(QuizaldoDbContext context)
        {
            this.context = context;
        }
    }
}
