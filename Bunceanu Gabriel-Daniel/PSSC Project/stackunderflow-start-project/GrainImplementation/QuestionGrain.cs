using GrainInterfaces;
using StackUnderflow.EF.Models;
using System.Threading.Tasks;

namespace GrainImplementation
{
    public class QuestionGrain : Orleans.Grain, IQuestionGrain
    {
        public async Task OnActivateAsync() { await base.OnActivateAsync(); }

        private readonly StackUnderflowContext _dbContext;
        private QuestionGrain state;

        public QuestionGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
