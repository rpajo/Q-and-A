using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Models;

namespace API
{
    public partial class questionoverflowContext : DbContext
    {
        public string connectionString = @"Server=localhost;User Id=root;Password=admin;Database=questionoverflow";
        //public string connectionString = @"Server=questionoverflow.ckztk2rxcoxz.us-west-2.rds.amazonaws.com;User Id=admin;Password=iamroot1;Database=questionoverflow";
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.UserId })
                    .HasName("PK_answers");

                entity.ToTable("answers");

                entity.HasIndex(e => e.AnswerId)
                    .HasName("answerId_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionId)
                    .HasName("questionIdAnswer");

                entity.HasIndex(e => e.UserId)
                    .HasName("userId_idx");

                entity.Property(e => e.AnswerId)
                    .HasColumnName("answerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Solved)
                    .HasColumnName("solved")
                    .HasColumnType("int(10)")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AnswersNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("userIdAnswer");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => new { e.CommentId, e.QuestionId, e.UserId })
                    .HasName("PK_comments");

                entity.ToTable("comments");

                entity.HasIndex(e => e.CommentId)
                    .HasName("commentId_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionId)
                    .HasName("questionId_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("userId_idx");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("tinytext");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parentId")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("userIdComment");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.UserId })
                    .HasName("PK_questions");

                entity.ToTable("questions");

                entity.HasIndex(e => e.UserId)
                    .HasName("userId_idx");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Anonymous)
                    .HasColumnName("anonymous")
                    .HasColumnType("int(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Answers)
                    .HasColumnName("answers")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuestionsNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("userId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("userId_UNIQUE");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Answers)
                    .HasColumnName("answers")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("char(256)");

                entity.Property(e => e.Questions)
                    .HasColumnName("questions")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Reputation)
                    .HasColumnName("reputation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(45)");
            });
        }
    }
}