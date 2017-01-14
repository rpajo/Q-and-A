using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using API;

namespace API.Migrations
{
    [DbContext(typeof(questionoverflowContext))]
    [Migration("20170114101937_questionOverflow")]
    partial class questionOverflow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("API.Models.Answers", b =>
                {
                    b.Property<int>("AnswerId")
                        .HasColumnName("answerId")
                        .HasColumnType("int(11)");

                    b.Property<int>("QuestionId")
                        .HasColumnName("questionId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("UserId")
                        .HasColumnName("userId")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("mediumtext");

                    b.Property<int?>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rating")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.HasKey("AnswerId", "QuestionId", "UserId")
                        .HasName("PK_answers");

                    b.HasIndex("AnswerId")
                        .IsUnique()
                        .HasName("answerId_UNIQUE");

                    b.HasIndex("QuestionId")
                        .HasName("questionIdAnswer");

                    b.HasIndex("UserId")
                        .HasName("userId_idx");

                    b.ToTable("answers");
                });

            modelBuilder.Entity("API.Models.Comments", b =>
                {
                    b.Property<int>("CommentId")
                        .HasColumnName("commentId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("QuestionId")
                        .HasColumnName("questionId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("UserId")
                        .HasColumnName("userId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnName("author")
                        .HasColumnType("varchar(45)");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("tinytext");

                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("parentId")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.HasKey("CommentId", "QuestionId", "UserId")
                        .HasName("PK_comments");

                    b.HasIndex("CommentId")
                        .IsUnique()
                        .HasName("commentId_UNIQUE");

                    b.HasIndex("QuestionId")
                        .HasName("questionId_idx");

                    b.HasIndex("UserId")
                        .HasName("userId_idx");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("API.Models.Questions", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnName("questionId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("UserId")
                        .HasColumnName("userId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int>("Anonymous")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("anonymous")
                        .HasColumnType("int(10)")
                        .HasDefaultValueSql("0");

                    b.Property<int>("Answers")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("answers")
                        .HasColumnType("int(10) unsigned")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("mediumtext");

                    b.Property<int>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rating")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(100)");

                    b.HasKey("QuestionId", "UserId")
                        .HasName("PK_questions");

                    b.HasIndex("UserId")
                        .HasName("userId_idx");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("API.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("userId")
                        .HasColumnType("int(10) unsigned");

                    b.Property<int?>("Answers")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("answers")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Location")
                        .HasColumnName("location")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("MemberSince");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("char(64)");

                    b.Property<int?>("Questions")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("questions")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<int?>("Reputation")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("reputation")
                        .HasColumnType("int(11)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("varchar(45)");

                    b.HasKey("UserId")
                        .HasName("userId_UNIQUE");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("email_UNIQUE");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("username_UNIQUE");

                    b.ToTable("users");
                });

            modelBuilder.Entity("API.Models.Answers", b =>
                {
                    b.HasOne("API.Models.Users", "User")
                        .WithMany("AnswersNavigation")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("API.Models.Comments", b =>
                {
                    b.HasOne("API.Models.Users", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("API.Models.Questions", b =>
                {
                    b.HasOne("API.Models.Users", "User")
                        .WithMany("QuestionsNavigation")
                        .HasForeignKey("UserId");
                });
        }
    }
}
