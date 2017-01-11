using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Users
    {
        public Users()
        {
            AnswersNavigation = new HashSet<Answers>();
            Comments = new HashSet<Comments>();
            QuestionsNavigation = new HashSet<Questions>();
        }

        public int UserId { get; set; }
        public int? Answers { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
        public int? Questions { get; set; }
        public int? Reputation { get; set; }
        public string Username { get; set; }
        public DateTime MemberSince { get; set; }

        public virtual ICollection<Answers> AnswersNavigation { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Questions> QuestionsNavigation { get; set; }
    }
}
