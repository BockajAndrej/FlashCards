using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Dal;

public class FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Card { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<CardCollectionEntity> Collection { get; set; }
    public DbSet<CompletedLessonEntity> CompletedLesson { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<CardEntity>()
        //     .HasOne(l => l.CardCollection)
        //     .WithMany(c => c.Cards)
        //     .HasForeignKey(l => l.CardCollectionId);
    }
}