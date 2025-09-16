using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class CompletedLessonEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public DateTime DatumVytvoreniaDateTime { get; set; }
    [Required]
    //It is designed to be used with EnumAbsolvovaneLekceTypOdpovedi
    public ICollection<int> PocetOdpovediDanehoTypu { get; set; } = new List<int>(Enum.GetNames(typeof(EnumCompletedLessonAnswerType)).Length);

    [Required]
    public Guid KolekceKaretId { get; set; }
    [Required]
    public CardCollectionEntity CardCollectionEntity { get; set; } = null!;
    [Required]
    public Guid UzivatelId { get; set; }
    [Required]
    public UserEntity UserEntity { get; set; } = null!;
}