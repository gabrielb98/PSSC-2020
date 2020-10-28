﻿using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using System.Linq;
using LanguageExt.Common;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class Votes
    {
        public interface IVotes { }

        public class UnverifiedVotes : IVotes
        {
            public int Votes { get; private set; }
            private UnverifiedVotes(int votes)
            {
                Votes = votes;
            }

            public static Result<UnverifiedVotes> Create(int votes)
            {
                if (ValidVotes(votes))
                {
                    return new UnverifiedVotes(votes);
                }
                else
                {
                    return new Result<UnverifiedVotes>(new InvalidVotesException(votes));
                }
            }

            private static bool ValidVotes(int votes)
            {
                if (votes != 0)
                {
                    return true;
                }
                return false;
            }
        }

        public class VerifiedVotes : IVotes
        {
            public int Votes { get; private set; }
            internal VerifiedVotes(int votes)
            {
                Votes = votes;
            }
        }
    }
}
