using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Answers
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int Solved { get; set; }
        public virtual Users User { get; set; }
    }
}
