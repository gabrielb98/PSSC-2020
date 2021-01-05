using StackUnderflow.EF.Models;
using System.Collections.Generic;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<Post> Posts { get; }

        public QuestionWriteContext(ICollection<Post> posts)
        {
            Posts = posts ?? new List<Post>(0);

        }
    }
}
