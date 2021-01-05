using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Access.Primitives.IO;
using LanguageExt;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using static StackUnderflow.Domain.Schema.Question.CreateQuestionOp.CreateQuestionResult;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Schema.Question.CreateReplyOp;

namespace StackUnderflow.Domain.Schema.Question.CreateQuestionOp
{
    public partial class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext _ex;

        public CreateQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<ICreateQuestionResult> Work(CreateQuestionCmd command, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in command.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(command))
                           select t;


            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }
       
        public ICreateQuestionResult AddQuestionIfMissing(QuestionWriteContext state, Post post)
        {
            if (state.Posts.Any(p => p.Title.Equals(post.Title)))
                return new QuestionNotCreated();

            if (state.Posts.All(p => p.PostId != post.PostId))
                state.Posts.Add(post);
            return new QuestionCreated(post);
        }

        private Post CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var question = new Post()
            {
                TenantId = cmd.TenantID,
                Title = cmd.Title,
                PostText = cmd.Body,
                PostedBy = cmd.OwnerID,
                PostTypeId = 1
            };
            question.PostTag.Add(new PostTag()
            {
                TenantId = cmd.TenantID,
                PostId = question.PostId,
                
                T = new Tag()
                {
                    TenantId = cmd.TenantID,
                    Name = cmd.Tags,
                }
            });
            return question;
        }

        public override Task PostConditions(CreateQuestionCmd op, ICreateQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
