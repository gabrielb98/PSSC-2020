﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Question.SendQuestionOwnerAcknowledgementOp
{
    public class SendQuestionOwnerAckCmd
    {
        public SendQuestionOwnerAckCmd(int questionId, int questionOwnerId)
        {
            QuestionId = questionId;
            QuestionOwnerId = questionOwnerId;
        }

        public int QuestionId { get; }
        public int QuestionOwnerId { get; }
    }
}