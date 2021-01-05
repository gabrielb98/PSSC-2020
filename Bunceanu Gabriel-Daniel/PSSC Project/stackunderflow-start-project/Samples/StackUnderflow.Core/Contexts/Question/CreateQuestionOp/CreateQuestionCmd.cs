using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Access.Primitives.IO;
using EarlyPay.Primitives.ValidationAttributes;

namespace StackUnderflow.Domain.Schema.Question.CreateQuestionOp
{
    public class CreateQuestionCmd
    {
        public CreateQuestionCmd(Guid ownerID, int tenantID, string title, string body, string tags)
        {
            OwnerID = ownerID;
            TenantID = tenantID;
            Title = title;
            Body = body;
            Tags = tags;
        }

        [GuidNotEmpty]
        public Guid OwnerID { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
        [Required]
        public string Tags { get; set; }
    } 
  }
