using System;
using System.Collections.Generic;
using System.Text;
using Question.Domain.CreateQuestionWorkflow;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;

namespace Test.App
{
    public class TestQuestion
    {
        public static void QuestionMain()
        {
            var cmd = new CreateQuestionCmd("Test Title", "Test Body", new string[] {"c#","python", "c++","java"});
            var result = CreateQuestion(cmd);
            result.Match(
                ProcessQuestionCreated,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
                );
            Console.ReadLine();
        }
        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }
        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.Question_ID}");
            return question;
        }
        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title) || string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Invalid title or description" };
                return new QuestionValidationFailed(errors);
            }
            if (new Random().Next(3) > 1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }
            var questionId = Guid.NewGuid();
            var results = new QuestionCreated(questionId, createQuestionCommand.Title, "test.bunceanu@gmail.com", true);
            return results;
        }
    }
}
