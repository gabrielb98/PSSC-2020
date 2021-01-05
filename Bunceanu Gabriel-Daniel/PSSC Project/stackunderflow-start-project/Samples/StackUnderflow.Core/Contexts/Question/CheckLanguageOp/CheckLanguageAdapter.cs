using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Question.CheckLanguageOp;
using static StackUnderflow.Domain.Schema.Question.CheckLanguageOp.CheckLanguageResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Domain.Core.Contexts.Question.CheckLanguageOp
{
    public class CheckLanguageAdaptor : Adapter<CheckLanguageCmd, ICheckLanguageResult, QuestionWriteContext, QuestionDependencies>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, ICheckLanguageResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICheckLanguageResult> Work(CheckLanguageCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            return new ValidationSucceeded("Valid");
        }
    }
}