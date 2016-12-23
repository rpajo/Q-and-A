using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Answers
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
    }
}
