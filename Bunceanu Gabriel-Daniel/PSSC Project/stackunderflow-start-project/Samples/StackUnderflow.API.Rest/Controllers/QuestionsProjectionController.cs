using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.EF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions-projection")]
    public class QuestionsProjectionController : ControllerBase
    {
        private readonly IClusterClient clusterClient;

        public QuestionsProjectionController(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuestions()
        {
            //var questions = GetQuestionsFromDB();
            var questionsProjectionGrain = this.clusterClient.GetGrain<IQuestionProjectionGrain>("organisation1");
            var questions = await questionsProjectionGrain.GetQuestionsAsync();

            return Ok(questions);
        }

        private List<Post> GetQuestionsFromDB()
        {
            return new List<Post> {
            new Post
            {
                PostId = 1,
                PostText = "My question text"
            }
            };
        }
    }
}