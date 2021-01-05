using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using StackUnderflow.Domain.Schema.Question.SendReplyAuthorAcknowledgementOp;
using System;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Question.SendReplyAuthorAcknowledgementOp.SendReplyAuthorAckResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendReplyAuthorAcknowledgementOp
{
    class SendReplyAuthorAckAdaptor : Adapter<SendReplyAuthorAckCmd, ISendReplyAuthorAcknowledgementResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IClusterClient clusterClient;

        public SendReplyAuthorAckAdaptor(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }
        public override Task PostConditions(SendReplyAuthorAckCmd cmd, ISendReplyAuthorAcknowledgementResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendReplyAuthorAcknowledgementResult> Work(SendReplyAuthorAckCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var asyncHelloGrain = this.clusterClient.GetGrain<IAsyncHello>("user1");
            await asyncHelloGrain.StartAsync();

            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync("email@address.com");

            return new AcknowledgementSent(1, 2);
        }
    }
}