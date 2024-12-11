using System;
using System.Collections.Generic;
using EngSite.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngSite.Api.DataAccess;

public partial class EnglishSiteContext : DbContext
{
    public EnglishSiteContext()
    {
    }

    public EnglishSiteContext(DbContextOptions<EnglishSiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dictionary> Dictionaries { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<TeacherStudent> TeacherStudents { get; set; }

    public virtual DbSet<Text> Texts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDictionary> UserDictionaries { get; set; }

    public virtual DbSet<UserStat> UserStats { get; set; }

    public virtual DbSet<UserText> UserTexts { get; set; }

    public virtual DbSet<WordStorage> WordStorages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=big;Password=0x1802;Database=english_site");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dictionary_pk");

            entity.ToTable("dictionary");

            entity.HasIndex(e => new { e.Sentence, e.Translation }, "dictionary_un").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Sentence).HasColumnName("sentence");
            entity.Property(e => e.Translation).HasColumnName("translation");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_pk");

            entity.ToTable("document");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Megadocumentid).HasColumnName("megadocumentid");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.TeacherStudentId).HasColumnName("teacher-student-id");
            entity.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");

            entity.HasOne(d => d.TeacherStudent).WithMany(p => p.Documents)
                .HasForeignKey(d => d.TeacherStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("document_fk");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("forum_pk");

            entity.ToTable("forum");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AuthorName)
                .HasColumnType("character varying")
                .HasColumnName("authorName");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Message).HasColumnName("message");
        });

        modelBuilder.Entity<TeacherStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teacher_student_pk");

            entity.ToTable("teacher-student");

            entity.HasIndex(e => new { e.Teacherlogin, e.Studentlogin }, "teacher_student_un").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Studentlogin)
                .HasMaxLength(16)
                .HasColumnName("studentlogin");
            entity.Property(e => e.Teacherlogin)
                .HasMaxLength(16)
                .HasColumnName("teacherlogin");

            entity.HasOne(d => d.StudentloginNavigation).WithMany(p => p.TeacherStudentStudentloginNavigations)
                .HasForeignKey(d => d.Studentlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_student_fk_1");

            entity.HasOne(d => d.TeacherloginNavigation).WithMany(p => p.TeacherStudentTeacherloginNavigations)
                .HasForeignKey(d => d.Teacherlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_student_fk");
        });

        modelBuilder.Entity<Text>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("text_pk");

            entity.ToTable("text");

            entity.HasIndex(e => new { e.Name, e.Path }, "text_un").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Login)
                .HasMaxLength(16)
                .HasColumnName("login");
            entity.Property(e => e.Email)
                .HasMaxLength(32)
                .HasColumnName("email");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<UserDictionary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_dictionary_pk");

            entity.ToTable("user-dictionary");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SentenceId).HasColumnName("sentence_id");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(16)
                .HasColumnName("user_login");

            entity.HasOne(d => d.Sentence).WithMany(p => p.UserDictionaries)
                .HasForeignKey(d => d.SentenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_dictionary_fk_1");

            entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.UserDictionaries)
                .HasForeignKey(d => d.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_dictionary_fk");
        });

        modelBuilder.Entity<UserStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_stat_pk");

            entity.ToTable("user_stat");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AudioListened)
                .HasDefaultValue(0)
                .HasColumnName("audio_listened");
            entity.Property(e => e.GrammarProgression)
                .HasDefaultValue(0)
                .HasColumnName("grammar_progression");
            entity.Property(e => e.TextsRead)
                .HasDefaultValue(0)
                .HasColumnName("texts_read");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(16)
                .HasColumnName("user_login");
            entity.Property(e => e.VideoWatched)
                .HasDefaultValue(0)
                .HasColumnName("video_watched");
            entity.Property(e => e.WordsLearned)
                .HasDefaultValue(0)
                .HasColumnName("words_learned");

            entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.UserStats)
                .HasForeignKey(d => d.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_stat_fk");
        });

        modelBuilder.Entity<UserText>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_text_pk");

            entity.ToTable("user-text");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TextId).HasColumnName("text_id");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(16)
                .HasColumnName("user_login");

            entity.HasOne(d => d.Text).WithMany(p => p.UserTexts)
                .HasForeignKey(d => d.TextId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_text_fk_1");

            entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.UserTexts)
                .HasForeignKey(d => d.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_text_fk");
        });

        modelBuilder.Entity<WordStorage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("word_storage");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Word)
                .HasColumnType("character varying")
                .HasColumnName("word");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
