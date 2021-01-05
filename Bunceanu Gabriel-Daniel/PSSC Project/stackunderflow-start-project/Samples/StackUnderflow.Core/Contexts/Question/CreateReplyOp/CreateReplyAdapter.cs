using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Schema.Question.CheckLanguageOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Question.CreateReplyOp.CreateReplyResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateReplyOp
{
    public class CreateReplyAdaptor : Adapter<CheckLanguageCmd, ICreateReplyResult, QuestionWriteContext, QuestionDependencies>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, ICreateReplyResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICreateReplyResult> Work(CheckLanguageCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddAnswerToQuestion(state, CreateAnswerFromCmd(cmd))
                           select t;

            //state.Replies.Add(new DatabaseModel.Models.Reply { AuthorUserId=55, Body="bodyyy 10", QuestionId=50, ReplyId=15});

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new ReplyNotCreated(ex.Message)
                );

            return result;
        }

        private ICreateReplyResult AddAnswerToQuestion(QuestionWriteContext state, object v)
        {
            return new ReplyCreated(1, 2, 3, "My answer body");
        }

        private object CreateAnswerFromCmd(CheckLanguageCmd cmd)
        {
            return new { };
        }
    }
}