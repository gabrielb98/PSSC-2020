using Access.Primitives.EFCore;
using Access.Primitives.IO;
using GrainInterfaces;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using StackUnderflow.Domain.Schema.Question.CheckLanguageOp;
using StackUnderflow.Domain.Schema.Question.CreateQuestionOp;
using StackUnderflow.EF.Models;
using System;
using System.Threading.Tasks;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("question")]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        [HttpPost("createquestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd createQuestionCmd)
        {
            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post));

            var dependencies = new QuestionDependencies();

            var expr = from createQuestionResult in QuestionDomain.CreateQuestion(createQuestionCmd)
// let adminUser = createQuestionResult.SafeCast<CreateQuestionResult.TenantCreated>().Select(p => p.AdminUser)
//                      let inviteAdminCmd = new InviteTenantAdminCmd(adminUser)
            from checkLanguageResult in QuestionDomain.CheckLanguage(new CheckLanguageCmd(createQuestionCmd.Body))
                       select new { createQuestionResult, checkLanguageResult };

            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.createQuestionResult.Match(
                created => (IActionResult)Ok("OK"),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Tenant could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));
        }

        
        private TryAsync<InvitationAcknowledgement> SendEmail(InvitationLetter letter)
        => async () =>
        {
            var emialSender = _client.GetGrain<IEmailSender>(0);
            await emialSender.SendEmailAsync(letter.Letter);
            return new InvitationAcknowledgement(Guid.NewGuid().ToString());
        };
        
    }
}

