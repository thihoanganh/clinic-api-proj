using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic_Web_Api.Migrations
{
    public partial class ChangeStaffRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountEvent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    MoneyCondition = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountEvent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LectureCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Bonus = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Origin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Salary = table.Column<long>(type: "bigint", nullable: true),
                    Allowance = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeminarEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeminarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarEmail", x => x.Id);
                    table.UniqueConstraint("AK_SeminarEmail_SeminarId", x => x.SeminarId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfMedicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfMedicine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Password = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    Sumary = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifyBy = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_LectureCategory",
                        column: x => x.CateId,
                        principalTable: "LectureCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    illustration = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    inventedYear = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    OriginId = table.Column<int>(type: "int", nullable: true),
                    MachineCategoryId = table.Column<int>(type: "int", nullable: true),
                    Priceid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificEquipment_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificEquipment_MachineCategory",
                        column: x => x.MachineCategoryId,
                        principalTable: "MachineCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificEquipment_Origin",
                        column: x => x.OriginId,
                        principalTable: "Origin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificEquipment_Price",
                        column: x => x.Priceid,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Username = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    WokingStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seminar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    Speaker = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Method = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Contact = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Poster = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seminar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seminar_SeminarEmail1",
                        column: x => x.Id,
                        principalTable: "SeminarEmail",
                        principalColumn: "SeminarId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    illustration = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Ingredient = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    PresentationFormat = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Point = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Using = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SpecialWarning = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    DateOfManufacture = table.Column<DateTime>(type: "datetime", nullable: true),
                    Expiry = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    OriginId = table.Column<int>(type: "int", nullable: true),
                    TypeOfId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    Priceid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicine_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicine_Origin",
                        column: x => x.OriginId,
                        principalTable: "Origin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicine_Price",
                        column: x => x.Priceid,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicine_TypeOfMedicine",
                        column: x => x.TypeOfId,
                        principalTable: "TypeOfMedicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    DiscountEventId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_DetailOrder_Customer",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailOrder_DiscountEvent",
                        column: x => x.DiscountEventId,
                        principalTable: "DiscountEvent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureComment_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureComment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    LevelId = table.Column<int>(type: "int", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    TotalQuestion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quiz_Level",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptScientificEquipment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    PriceBuy = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ScientificEquipmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptScientificEquipment", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReceiptScientificEquipment_ScientificEquipment",
                        column: x => x.ScientificEquipmentId,
                        principalTable: "ScientificEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Place = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_Staff",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
                    SatisfiedPercent = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SeminarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Seminar",
                        column: x => x.SeminarId,
                        principalTable: "Seminar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeminarRegistation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FName = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    SeminarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarRegistation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeminarRegistation_Seminar",
                        column: x => x.SeminarId,
                        principalTable: "Seminar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptMedicine",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<int>(type: "int", nullable: true),
                    PriceBuy = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    MedicineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptMedicine", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReceiptMedicine_Medicine",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
                    CorrectAnsw = table.Column<int>(type: "int", nullable: true),
                    QuizId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Quiz",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserQuiz",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalQuestion = table.Column<int>(type: "int", nullable: true),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: true),
                    NoAnswer = table.Column<int>(type: "int", nullable: true),
                    Percent = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuiz", x => new { x.QuizId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserQuiz_Quiz",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserQuiz_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptScientificEquipmentId_OrderDetail",
                columns: table => new
                {
                    ReceiptScientificEquipmentId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptScientificEquipmentId_OrderDetail", x => new { x.ReceiptScientificEquipmentId, x.OrderDetailId });
                    table.ForeignKey(
                        name: "FK_ReceiptScientificEquipmentId_OrderDetail_DetailOrder",
                        column: x => x.OrderDetailId,
                        principalTable: "DetailOrder",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptScientificEquipmentId_OrderDetail_ReceiptScientificEquipment",
                        column: x => x.ReceiptScientificEquipmentId,
                        principalTable: "ReceiptScientificEquipment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptMedicineId_Orderdetail",
                columns: table => new
                {
                    ReceiptMedicineId = table.Column<int>(type: "int", nullable: false),
                    OrderdetailId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptMedicineId_Orderdetail", x => new { x.ReceiptMedicineId, x.OrderdetailId });
                    table.ForeignKey(
                        name: "FK_ReceiptMedicineId_Orderdetail_DetailOrder",
                        column: x => x.OrderdetailId,
                        principalTable: "DetailOrder",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptMedicineId_Orderdetail_ReceiptMedicine",
                        column: x => x.ReceiptMedicineId,
                        principalTable: "ReceiptMedicine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nchar(300)", fixedLength: true, maxLength: 300, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LectureId",
                table: "Attachment",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_StaffId",
                table: "Certificate",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrder_CustomerId",
                table: "DetailOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrder_DiscountEventId",
                table: "DetailOrder",
                column: "DiscountEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_SeminarId",
                table: "Feedback",
                column: "SeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_CateId",
                table: "Lecture",
                column: "CateId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureComment_LectureId",
                table: "LectureComment",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureComment_UserId",
                table: "LectureComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_BrandId",
                table: "Medicine",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_OriginId",
                table: "Medicine",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Priceid",
                table: "Medicine",
                column: "Priceid");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_TypeOfId",
                table: "Medicine",
                column: "TypeOfId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuizId",
                table: "Question",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_LectureId",
                table: "Quiz",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_LevelId",
                table: "Quiz",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptMedicine_MedicineId",
                table: "ReceiptMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptMedicineId_Orderdetail_OrderdetailId",
                table: "ReceiptMedicineId_Orderdetail",
                column: "OrderdetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptScientificEquipment_ScientificEquipmentId",
                table: "ReceiptScientificEquipment",
                column: "ScientificEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptScientificEquipmentId_OrderDetail_OrderDetailId",
                table: "ReceiptScientificEquipmentId_OrderDetail",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificEquipment_BrandId",
                table: "ScientificEquipment",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificEquipment_MachineCategoryId",
                table: "ScientificEquipment",
                column: "MachineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificEquipment_OriginId",
                table: "ScientificEquipment",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificEquipment_Priceid",
                table: "ScientificEquipment",
                column: "Priceid");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarEmail",
                table: "SeminarEmail",
                column: "SeminarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeminarRegistation_SeminarId",
                table: "SeminarRegistation",
                column: "SeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PositionId",
                table: "Staff",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoleId",
                table: "Staff",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LectureId",
                table: "Tag",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuiz_UserId",
                table: "UserQuiz",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "LectureComment");

            migrationBuilder.DropTable(
                name: "ReceiptMedicineId_Orderdetail");

            migrationBuilder.DropTable(
                name: "ReceiptScientificEquipmentId_OrderDetail");

            migrationBuilder.DropTable(
                name: "SeminarRegistation");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "UserQuiz");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "ReceiptMedicine");

            migrationBuilder.DropTable(
                name: "DetailOrder");

            migrationBuilder.DropTable(
                name: "ReceiptScientificEquipment");

            migrationBuilder.DropTable(
                name: "Seminar");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DiscountEvent");

            migrationBuilder.DropTable(
                name: "ScientificEquipment");

            migrationBuilder.DropTable(
                name: "SeminarEmail");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "TypeOfMedicine");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "MachineCategory");

            migrationBuilder.DropTable(
                name: "Origin");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "LectureCategory");
        }
    }
}
