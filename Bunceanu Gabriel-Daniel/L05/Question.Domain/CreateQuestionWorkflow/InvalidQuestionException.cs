using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [Serializable]
    public class InvalidQuestionException : Exception
    {
        public InvalidQuestionException()
        {

        }

        public InvalidQuestionException(string question) : base($"The value \"{question}\" can't have more than 1000 characters.")
        {

        }

        public InvalidQuestionException(List<string> tag) : base($"The tag is \"{tag.Count}\".Must have at least one tag and at most 3!")
        {

        }

    }
}
