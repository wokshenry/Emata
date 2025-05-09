using Emata.API.Context;
using Emata.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Emata.API.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetRandomQuestionsAsync(int count);
        Task<Question?> GetQuestionByIdAsync(int id);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Question>> GetRandomQuestionsAsync(int count)
        {
            List<Question> questions = new List<Question>();
            Random random = new Random();
            // Get random questions from the database

            var _Questions = await _dbContext.Questions.OrderBy(o=> o.Id).ToListAsync();

            var length = (_Questions.Count -1);

            while (questions.Count < count) 
            {
                var a = _Questions[random.Next(0, length)];
                var exists = questions.FirstOrDefault(x=>  x.Id == a.Id);
                if (exists == null)
                {
                    questions.Add(a);
                }
            }
            return questions;
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _dbContext.Questions.FindAsync(id);
        }
    }
}
