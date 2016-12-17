using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class questionoverflowContext : DbContext
    {
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public questionoverflowContext(DbContextOptions<questionoverflowContext> options) : base(options)
        {

        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost; user id=root; password=admin; persist security info=True;database=questionoverflow;");
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => new { e.AnswerId, e.QuestionId })
                    .HasName("PK_answers");

                entity.ToTable("answers");

                entity.HasIndex(e => e.AnswerId)
                    .HasName("answerId_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionId)
                    .HasName("questionId");

                entity.Property(e => e.AnswerId)
                    .HasColumnName("answerId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => new { e.CommentId, e.QuestionId })
                    .HasName("PK_comments");

                entity.ToTable("comments");

                entity.HasIndex(e => e.CommentId)
                    .HasName("commentId_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionId)
                    .HasName("questionId_idx");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parentId")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("varchar(45)");
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

                entity.Property(e => e.Anonymous)
                    .IsRequired()
                    .HasColumnName("anonymous")
                    .HasColumnType("binary(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("int(10) unsigned zerofill")
                    .HasDefaultValueSql("0000000000");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(10) unsigned zerofill")
                    .HasDefaultValueSql("0000000000");

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

                entity.Property(e => e.MemberSince)
                    .HasColumnName("memberSince")
                    .HasColumnType("date");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("char(64)");

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