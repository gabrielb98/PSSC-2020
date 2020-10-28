using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using System.Linq;
using LanguageExt.Common;
using LanguageExt;
using static Question.Domain.CreateQuestionWorkflow.Question;
using Question.Domain.CreateQuestionWorkflow;

namespace Test.App
{
    class ProgramQuestion
    {
        static void MainQuestion(string[] args)
        {
            List<string> tags = new List<string>() { "Azure", "Google Cloud Platform", "Amazon Web Services" };

            var result = UnverifiedQuestion.Create_question("What's the best cloud platform for enterprise?", tags);

            result.Match(
                Succ: question =>
                {
                    VoteQuestion(question);
                    Console.WriteLine("You can vote this question!");
                    return Unit.Default;
                },
                Fail: ex =>
                {
                    Console.WriteLine($"Question could not be posted. Reason: {ex.Message}");
                    return Unit.Default;
                }
                );
            Console.ReadLine();
        }
        private static void VoteQuestion(UnverifiedQuestion question)
        {
            var verifiedQuestionResult = new VerifyQuestionService().VerifyQuestion(question);
            verifiedQuestionResult.Match(
                    VoteQuestion =>
                    {
                        new VoteService().Vote(VoteQuestion);
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("You can't vote this question!");
                        return Unit.Default;
                    }
                );

        }
    }
}
