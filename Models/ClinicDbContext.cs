using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Clinic_Web_Api.Models
{
    public partial class ClinicDbContext : DbContext
    {
        public ClinicDbContext()
        {
        }

        public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<DetailOrder> DetailOrders { get; set; }
        public virtual DbSet<DiscountEvent> DiscountEvents { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<LectureCategory> LectureCategories { get; set; }
        public virtual DbSet<LectureComment> LectureComments { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<MachineCategory> MachineCategories { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Origin> Origins { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<PriceMedicine> PriceMedicines { get; set; }
        public virtual DbSet<PriceScientificEquipment> PriceScientificEquipments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<ReceiptMedicine> ReceiptMedicines { get; set; }
        public virtual DbSet<ReceiptMedicineIdOrderdetail> ReceiptMedicineIdOrderdetails { get; set; }
        public virtual DbSet<ReceiptScientificEquipment> ReceiptScientificEquipments { get; set; }
        public virtual DbSet<ReceiptScientificEquipmentIdOrderDetail> ReceiptScientificEquipmentIdOrderDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScientificEquipment> ScientificEquipments { get; set; }
        public virtual DbSet<Seminar> Seminars { get; set; }
        public virtual DbSet<SeminarEmail> SeminarEmails { get; set; }
        public virtual DbSet<SeminarRegistation> SeminarRegistations { get; set; }
        public virtual DbSet<TypeOfMedicine> TypeOfMedicines { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserQuiz> UserQuizzes { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Clinic1;user id=sa;password=123123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Content).HasMaxLength(300);

                entity.Property(e => e.IsCorrect)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.OriginName).HasMaxLength(500);

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Attachment_Lecture");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Brand1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Brand");
            });

            modelBuilder.Entity<DetailOrder>(entity =>
            {
                entity.ToTable("DetailOrder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.DetailOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_DetailOrder_Customer");

                entity.HasOne(d => d.DiscountEvent)
                    .WithMany(p => p.DetailOrders)
                    .HasForeignKey(d => d.DiscountEventId);
            });

            modelBuilder.Entity<DiscountEvent>(entity =>
            {
                entity.ToTable("DiscountEvent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Seminar)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.SeminarId)
                    .HasConstraintName("FK_Feedback_Seminar");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lecture");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.ModifyBy).HasMaxLength(200);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Sumary).HasMaxLength(1000);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK_Lecture_LectureCategory");
            });

            modelBuilder.Entity<LectureCategory>(entity =>
            {
                entity.ToTable("LectureCategory");

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<LectureComment>(entity =>
            {
                entity.ToTable("LectureComment");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.LectureComments)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_LectureComment_Lecture");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LectureComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LectureComment_User");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<MachineCategory>(entity =>
            {
                entity.ToTable("MachineCategory");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfManufacture).HasColumnType("datetime");

                entity.Property(e => e.Expiry).HasColumnType("datetime");

                entity.Property(e => e.Illustration)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("illustration");

                entity.Property(e => e.Ingredient).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Point).IsUnicode(false);

                entity.Property(e => e.PresentationFormat).IsUnicode(false);

                entity.Property(e => e.SpecialWarning).IsUnicode(false);

                entity.Property(e => e.Using).IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Medicine_Brand");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.OriginId)
                    .HasConstraintName("FK_Medicine_Origin");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.PriceId)
                    .HasConstraintName("FK_Medicine_Price");

                entity.HasOne(d => d.TypeOf)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.TypeOfId)
                    .HasConstraintName("FK_Medicine_TypeOfMedicine");
            });

            modelBuilder.Entity<Origin>(entity =>
            {
                entity.ToTable("Origin");

                entity.Property(e => e.Origin1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Origin");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("Price");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Price1).HasColumnName("Price");
            });

            modelBuilder.Entity<PriceMedicine>(entity =>
            {
                entity.ToTable("PriceMedicine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.PriceMedicines)
                    .HasForeignKey(d => d.MedicineId)
                    .HasConstraintName("FK_PriceMedicine_Medicine");
            });

            modelBuilder.Entity<PriceScientificEquipment>(entity =>
            {
                entity.ToTable("PriceScientificEquipment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.ScientificEquipment)
                    .WithMany(p => p.PriceScientificEquipments)
                    .HasForeignKey(d => d.ScientificEquipmentId)
                    .HasConstraintName("FK_PriceScientificEquipment_ScientificEquipment");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_Question_Quiz");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Quiz_Lecture");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK_Quiz_Level");
            });

            modelBuilder.Entity<ReceiptMedicine>(entity =>
            {
                entity.ToTable("ReceiptMedicine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.ReceiptMedicines)
                    .HasForeignKey(d => d.MedicineId)
                    .HasConstraintName("FK_ReceiptMedicine_Medicine");
            });

            modelBuilder.Entity<ReceiptMedicineIdOrderdetail>(entity =>
            {
                entity.HasKey(e => new { e.ReceiptMedicineId, e.OrderdetailId });

                entity.ToTable("ReceiptMedicineId_Orderdetail");

                entity.HasOne(d => d.Orderdetail)
                    .WithMany(p => p.ReceiptMedicineIdOrderdetails)
                    .HasForeignKey(d => d.OrderdetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptMedicineId_Orderdetail_DetailOrder");

                entity.HasOne(d => d.ReceiptMedicine)
                    .WithMany(p => p.ReceiptMedicineIdOrderdetails)
                    .HasForeignKey(d => d.ReceiptMedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptMedicineId_Orderdetail_ReceiptMedicine");
            });

            modelBuilder.Entity<ReceiptScientificEquipment>(entity =>
            {
                entity.ToTable("ReceiptScientificEquipment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.ScientificEquipment)
                    .WithMany(p => p.ReceiptScientificEquipments)
                    .HasForeignKey(d => d.ScientificEquipmentId)
                    .HasConstraintName("FK_ReceiptScientificEquipment_ScientificEquipment");
            });

            modelBuilder.Entity<ReceiptScientificEquipmentIdOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.ReceiptScientificEquipmentId, e.OrderDetailId });

                entity.ToTable("ReceiptScientificEquipmentId_OrderDetail");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.ReceiptScientificEquipmentIdOrderDetails)
                    .HasForeignKey(d => d.OrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptScientificEquipmentId_OrderDetail_DetailOrder");

                entity.HasOne(d => d.ReceiptScientificEquipment)
                    .WithMany(p => p.ReceiptScientificEquipmentIdOrderDetails)
                    .HasForeignKey(d => d.ReceiptScientificEquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptScientificEquipmentId_OrderDetail_ReceiptScientificEquipment");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ScientificEquipment>(entity =>
            {
                entity.ToTable("ScientificEquipment");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Illustration)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("illustration");

                entity.Property(e => e.InventedYear).HasColumnName("inventedYear");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ScientificEquipments)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_ScientificEquipment_Brand");

                entity.HasOne(d => d.MachineCategory)
                    .WithMany(p => p.ScientificEquipments)
                    .HasForeignKey(d => d.MachineCategoryId)
                    .HasConstraintName("FK_ScientificEquipment_MachineCategory");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.ScientificEquipments)
                    .HasForeignKey(d => d.OriginId)
                    .HasConstraintName("FK_ScientificEquipment_Origin");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.ScientificEquipments)
                    .HasForeignKey(d => d.Priceid)
                    .HasConstraintName("FK_ScientificEquipment_Price");
            });

            modelBuilder.Entity<Seminar>(entity =>
            {
                entity.ToTable("Seminar");

                entity.Property(e => e.Contact).HasMaxLength(300);

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.Method).HasMaxLength(100);

                entity.Property(e => e.Place).HasMaxLength(300);

                entity.Property(e => e.Poster).HasMaxLength(300);

                entity.Property(e => e.Speaker).HasMaxLength(100);

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<SeminarEmail>(entity =>
            {
                entity.ToTable("SeminarEmail");

                entity.Property(e => e.Title).HasMaxLength(300);

                entity.HasOne(d => d.Seminar)
                    .WithMany(p => p.SeminarEmails)
                    .HasForeignKey(d => d.SeminarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeminarEmail_Seminar");
            });

            modelBuilder.Entity<SeminarRegistation>(entity =>
            {
                entity.ToTable("SeminarRegistation");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fname)
                    .HasMaxLength(300)
                    .HasColumnName("FName");

                entity.HasOne(d => d.Seminar)
                    .WithMany(p => p.SeminarRegistations)
                    .HasForeignKey(d => d.SeminarId)
                    .HasConstraintName("FK_SeminarRegistation_Seminar");
            });

            modelBuilder.Entity<TypeOfMedicine>(entity =>
            {
                entity.ToTable("TypeOfMedicine");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.FullName).HasMaxLength(300);

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(300);
            });

            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.HasKey(e => new { e.QuizId, e.UserId });

                entity.ToTable("UserQuiz");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserQuiz_Quiz");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserQuiz_User");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.Property(e => e.WokingStart).HasColumnType("datetime");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Staff_Position");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RoleId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
