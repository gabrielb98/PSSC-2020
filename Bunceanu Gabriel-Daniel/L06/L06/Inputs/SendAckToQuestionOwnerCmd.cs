using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using System.ComponentModel.DataAnnotations;

namespace L06.Inputs
{
    class SendAckToQuestionOwnerCmd
    {
        [Required]
        public int ReplyId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        public string Answer { get; }

        public SendAckToQuestionOwnerCmd(int replyid, int questionid, string answer)
        {
            ReplyId = replyid;
            QuestionId = questionid;
            Answer = answer;
        }
    }
}
