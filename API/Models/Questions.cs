using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Questions
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int Anonymous { get; set; }
        public int Comments { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }

        public virtual Users User { get; set; }
    }
}
