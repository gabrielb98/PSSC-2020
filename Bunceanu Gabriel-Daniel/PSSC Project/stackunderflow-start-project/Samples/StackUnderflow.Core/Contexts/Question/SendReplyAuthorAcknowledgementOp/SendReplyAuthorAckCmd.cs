using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Question.SendReplyAuthorAcknowledgementOp
{
    public class SendReplyAuthorAckCmd
    {
        public SendReplyAuthorAckCmd(Guid replyAuthorId, int questionId, int answerId)
        {
            ReplyAuthorId = replyAuthorId;
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public Guid ReplyAuthorId { get; }
        public int QuestionId { get; }
        public int AnswerId { get; }
    }
}