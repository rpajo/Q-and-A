using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class questionOverflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    answers = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "varchar(200)", nullable: false),
                    location = table.Column<string>(type: "varchar(100)", nullable: true),
                    MemberSince = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(type: "char(64)", nullable: false),
                    questions = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    reputation = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    username = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("userId_UNIQUE", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    answerId = table.Column<int>(type: "int(11)", nullable: false),
                    questionId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    userId = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    description = table.Column<string>(type: "mediumtext", nullable: false),
                    rating = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => new { x.answerId, x.questionId, x.userId });
                    table.ForeignKey(
                        name: "FK_answers_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    questionId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    userId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    author = table.Column<string>(type: "varchar(45)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    description = table.Column<string>(type: "tinytext", nullable: false),
                    parentId = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => new { x.commentId, x.questionId, x.userId });
                    table.ForeignKey(
                        name: "FK_comments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    questionId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    userId = table.Column<int>(type: "int(10) unsigned", nullable: false),
                    anonymous = table.Column<int>(type: "int(10)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    answers = table.Column<int>(type: "int(10) unsigned", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(type: "mediumtext", nullable: false),
                    rating = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "0")
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    title = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => new { x.questionId, x.userId });
                    table.ForeignKey(
                        name: "FK_questions_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "answerId_UNIQUE",
                table: "answers",
                column: "answerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "questionIdAnswer",
                table: "answers",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "userId_idx",
                table: "answers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "commentId_UNIQUE",
                table: "comments",
                column: "commentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "questionId_idx",
                table: "comments",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "userId_idx",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "userId_idx",
                table: "questions",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "email_UNIQUE",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "username_UNIQUE",
                table: "users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
