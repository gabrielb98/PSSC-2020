using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using System.Dynamic;
using System.Linq;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid Question_ID { get; private set; }
            public string Email { get; private set; }
            public string Question { get; private set; }
            public bool ApprovedbyML { get; set; }


            public QuestionCreated(Guid Q_ID, string question, string email, bool ML_Approved)
            {
                Question_ID = Q_ID;
                Question = question;
                Email = email;
                ApprovedbyML = ML_Approved;
            }
        }
        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; set; }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}
