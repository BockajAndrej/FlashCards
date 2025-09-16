using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models;

public class CompletedLessonDetailModel : IEntity
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
    public CardCollectionDetailModel CardCollection { get; set; } = null!;
    [Required]
    public Guid UzivatelId { get; set; }
    [Required]
    public UserDetailModel User { get; set; } = null!;
}