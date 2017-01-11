using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Comments
    {
        public int CommentId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }

        public virtual Users User { get; set; }
    }
}
