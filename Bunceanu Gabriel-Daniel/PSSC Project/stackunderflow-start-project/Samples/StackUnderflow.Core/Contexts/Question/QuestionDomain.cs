using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Question.CheckLanguageOp;
using StackUnderflow.Domain.Schema.Question.CreateQuestionOp;
using StackUnderflow.Domain.Schema.Question.CreateReplyOp;
using StackUnderflow.Domain.Schema.Question.SendQuestionOwnerAcknowledgementOp;
using StackUnderflow.Domain.Schema.Question.SendReplyAuthorAcknowledgementOp;
using static PortExt;
using static StackUnderflow.Domain.Schema.Question.CheckLanguageOp.CheckLanguageResult;
using static StackUnderflow.Domain.Schema.Question.CreateQuestionOp.CreateQuestionResult;
using static StackUnderflow.Domain.Schema.Question.CreateReplyOp.CreateReplyResult;
using static StackUnderflow.Domain.Schema.Question.SendQuestionOwnerAcknowledgementOp.SendQuestionOwnerAckResult;
using static StackUnderflow.Domain.Schema.Question.SendReplyAuthorAcknowledgementOp.SendReplyAuthorAckResult;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public static class QuestionDomain
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd createQuestionCmd) =>
            NewPort<CreateQuestionCmd, ICreateQuestionResult>(createQuestionCmd);
      
        public static Port<ICreateReplyResult> CreateReply(CreateReplyCmd createReplyCmd) =>
           NewPort<CreateReplyCmd, ICreateReplyResult>(createReplyCmd);
      

        public static Port<ICheckLanguageResult> CheckLanguage(CheckLanguageCmd checkLanguageCmd) =>
            NewPort<CheckLanguageCmd, ICheckLanguageResult>(checkLanguageCmd);

        
        public static Port<ISendQuestionOwnerAcknowledgementResult> SendQuestionOwnerAcknowledgement(SendQuestionOwnerAckCmd cmd) =>
            NewPort<SendQuestionOwnerAckCmd, ISendQuestionOwnerAcknowledgementResult>(cmd);

        public static Port<ISendReplyAuthorAcknowledgementResult> SendReplyAuthorAcknowledgement(SendReplyAuthorAckCmd cmd) =>
           NewPort<SendReplyAuthorAckCmd, ISendReplyAuthorAcknowledgementResult>(cmd);
        
    }
}