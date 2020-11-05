using L06.Outputs;
using System;

namespace L06
{
    class Program
    {
        static void Main(string[] args)
        {
            var wf = from createReplyResult in Domain.ValidateReply(1, 1, "test...")
                     let validReply = (ValidateReplyResult.ReplyValidated)createReplyResult
                     from checkLanguageResult in Domain.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in Domain.SendAckToQuestionOwner(1, 1, "test...")
                     from authorAck in Domain.SendAckToReplyAuthor(2, 1, "test...")
                     select (validReply, checkLanguageResult, ownerAck, authorAck);


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

    }

    internal interface IReplyPosted
    {

    }
}

