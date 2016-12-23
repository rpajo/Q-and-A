using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { set; get; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int ParentId { get; set; }
    }
}
