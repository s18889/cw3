using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class s18889Context : DbContext
    {
        public s18889Context()
        {
        }

        public s18889Context(DbContextOptions<s18889Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<Dostawca> Dostawca { get; set; }
        public virtual DbSet<Emp> Emp { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Miasto> Miasto { get; set; }
        public virtual DbSet<Obowiazki> Obowiazki { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pracownik> Pracownik { get; set; }
        public virtual DbSet<Produkt> Produkt { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Refresh> Refresh { get; set; }
        public virtual DbSet<Salgrade> Salgrade { get; set; }
        public virtual DbSet<Sprzedaz> Sprzedaz { get; set; }
        public virtual DbSet<StOb> StOb { get; set; }
        public virtual DbSet<Stanowisko> Stanowisko { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TeamMember> TeamMember { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s18889;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DEPT");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Dname)
                    .HasColumnName("DNAME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Loc)
                    .HasColumnName("LOC")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dostawca>(entity =>
            {
                entity.HasKey(e => e.Nip)
                    .HasName("PK__Dostawca__DF97D0E991E93FC5");

                entity.Property(e => e.Nip)
                    .HasColumnName("nip")
                    .ValueGeneratedNever();

                entity.Property(e => e.EMail)
                    .HasColumnName("e_mail")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NrTelefinu).HasColumnName("nr_telefinu");

                entity.Property(e => e.OsobaOdpowiedzialnaZaKomunik).HasColumnName("Osoba_odpowiedzialna_za_komunik");

                entity.HasOne(d => d.OsobaOdpowiedzialnaZaKomunikNavigation)
                    .WithMany(p => p.Dostawca)
                    .HasForeignKey(d => d.OsobaOdpowiedzialnaZaKomunik)
                    .HasConstraintName("FK__Dostawca__Osoba___656C112C");
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EMP");

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Empno).HasColumnName("EMPNO");

                entity.Property(e => e.Ename)
                    .HasColumnName("ENAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("HIREDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Job)
                    .HasColumnName("JOB")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Mgr).HasColumnName("MGR");

                entity.Property(e => e.Sal).HasColumnName("SAL");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("KATEGORIA");

                entity.Property(e => e.IdKategoria).HasColumnName("ID_KATEGORIA");

                entity.Property(e => e.Kategoria1)
                    .IsRequired()
                    .HasColumnName("KATEGORIA")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("KLIENT");

                entity.Property(e => e.IdKlient).HasColumnName("ID_KLIENT");

                entity.Property(e => e.IdMiasto).HasColumnName("ID_MIASTO");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("IMIE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("NAZWISKO")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial)
                    .HasName("PK__Material__81E99B837156F400");

                entity.Property(e => e.IdMaterial)
                    .HasColumnName("id_material")
                    .ValueGeneratedNever();

                entity.Property(e => e.CalkowiteZapotrzebowanie).HasColumnName("calkowite_zapotrzebowanie");

                entity.Property(e => e.DostawcaNip).HasColumnName("Dostawca_nip");

                entity.Property(e => e.Jednostka)
                    .HasColumnName("jednostka")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ZapotrzebowanieNastepnyTyd).HasColumnName("zapotrzebowanie_nastepny_tyd");

                entity.HasOne(d => d.DostawcaNipNavigation)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.DostawcaNip)
                    .HasConstraintName("FK__Material__Dostaw__72C60C4A");
            });

            modelBuilder.Entity<Miasto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MIASTO");

                entity.Property(e => e.IdMiasto).HasColumnName("ID_MIASTO");

                entity.Property(e => e.Miasto1)
                    .IsRequired()
                    .HasColumnName("MIASTO")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Obowiazki>(entity =>
            {
                entity.HasKey(e => e.IdObowaizk)
                    .HasName("PK__Obowiazk__9DBCA9B46FFA05DE");

                entity.Property(e => e.IdObowaizk)
                    .HasColumnName("id_obowaizk")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.HasKey(e => e.Pesel)
                    .HasName("PK__Osoba__DC3B1BB9238F294B");

                entity.Property(e => e.Pesel)
                    .HasColumnName("pesel")
                    .ValueGeneratedNever();

                entity.Property(e => e.Imie)
                    .HasColumnName("imie")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .HasColumnName("nazwisko")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pensja).HasColumnName("pensja");

                entity.Property(e => e.Przelozony).HasColumnName("przelozony");

                entity.Property(e => e.Stanowisko).HasColumnName("stanowisko");

                entity.HasOne(d => d.PrzelozonyNavigation)
                    .WithMany(p => p.InversePrzelozonyNavigation)
                    .HasForeignKey(d => d.Przelozony)
                    .HasConstraintName("FK__Osoba__przelozon__74AE54BC");
            });

            modelBuilder.Entity<Pracownik>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PRACOWNIK");

                entity.Property(e => e.IdMiasto).HasColumnName("ID_MIASTO");

                entity.Property(e => e.IdPracownik).HasColumnName("ID_PRACOWNIK");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("IMIE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("NAZWISKO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pensja).HasColumnName("PENSJA");
            });

            modelBuilder.Entity<Produkt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PRODUKT");

                entity.Property(e => e.Cena).HasColumnName("CENA");

                entity.Property(e => e.IdKategoria).HasColumnName("ID_KATEGORIA");

                entity.Property(e => e.IdProdukt).HasColumnName("ID_PRODUKT");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("NAZWA")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("Project_pk");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Refresh>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("refresh");

                entity.Property(e => e.Kay)
                    .HasColumnName("kay")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Salgrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SALGRADE");

                entity.Property(e => e.Grade).HasColumnName("GRADE");

                entity.Property(e => e.Hisal).HasColumnName("HISAL");

                entity.Property(e => e.Losal).HasColumnName("LOSAL");
            });

            modelBuilder.Entity<Sprzedaz>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SPRZEDAZ");

                entity.Property(e => e.DataSprzedazy)
                    .HasColumnName("DATA_SPRZEDAZY")
                    .HasColumnType("date");

                entity.Property(e => e.IdKlient).HasColumnName("ID_KLIENT");

                entity.Property(e => e.IdPracownik).HasColumnName("ID_PRACOWNIK");

                entity.Property(e => e.IdProdukt).HasColumnName("ID_PRODUKT");

                entity.Property(e => e.IdSprzedaz).HasColumnName("ID_SPRZEDAZ");

                entity.Property(e => e.Ilosc).HasColumnName("ILOSC");
            });

            modelBuilder.Entity<StOb>(entity =>
            {
                entity.HasKey(e => new { e.StanowiskoIdStano, e.ObowiazkiIdObowia })
                    .HasName("PK");

                entity.ToTable("st_ob");

                entity.Property(e => e.StanowiskoIdStano).HasColumnName("Stanowisko_id_stano");

                entity.Property(e => e.ObowiazkiIdObowia).HasColumnName("Obowiazki_id_obowia");

                entity.HasOne(d => d.ObowiazkiIdObowiaNavigation)
                    .WithMany(p => p.StOb)
                    .HasForeignKey(d => d.ObowiazkiIdObowia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__st_ob__Obowiazki__01142BA1");

                entity.HasOne(d => d.StanowiskoIdStanoNavigation)
                    .WithMany(p => p.StOb)
                    .HasForeignKey(d => d.StanowiskoIdStano)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__st_ob__Stanowisk__00200768");
            });

            modelBuilder.Entity<Stanowisko>(entity =>
            {
                entity.HasKey(e => e.IdStanowisk)
                    .HasName("PK__Stanowis__9ACEC84D8CCE2D00");

                entity.Property(e => e.IdStanowisk)
                    .HasColumnName("id_stanowisk")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pasword)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("Task_pk");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdAssignedToNavigation)
                    .WithMany(p => p.TaskIdAssignedToNavigation)
                    .HasForeignKey(d => d.IdAssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember2");

                entity.HasOne(d => d.IdCreatorNavigation)
                    .WithMany(p => p.TaskIdCreatorNavigation)
                    .HasForeignKey(d => d.IdCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember1");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Project");

                entity.HasOne(d => d.IdTaskTypeNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdTaskType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TaskType");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.IdTaskType)
                    .HasName("TaskType_pk");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.IdTeamMember)
                    .HasName("TeamMember_pk");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
