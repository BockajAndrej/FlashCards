using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Dal;

public class FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Karty { get; set; }
    public DbSet<UserEntity> Uzivatel { get; set; }
    public DbSet<CardCollectionEntity> KolekceKaret { get; set; }
    public DbSet<CompletedLessonEntity> AbsolvovaneLekce { get; set; }
}