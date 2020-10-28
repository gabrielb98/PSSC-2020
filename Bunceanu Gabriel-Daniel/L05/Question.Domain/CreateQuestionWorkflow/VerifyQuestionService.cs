using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using System.Linq;
using LanguageExt.Common;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class VerifyQuestionService
    {
        public Result<VerifiedQuestion> VerifyQuestion(UnverifiedQuestion question)
        {
            return new VerifiedQuestion(question.Question, question.Tag);
        }
    }
}
