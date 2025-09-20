using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CompletedLessonListModel : IEntityModel
{
    public Guid Id { get; set; }
    [Required]
    public DateTime CreatedDateTime { get; set; }
    [Required]
    //It is designed to be used with EnumAbsolvovaneLekceTypOdpovedi
    public ICollection<int> NumberOfCorrectAnswersByTypes { get; set; } = new List<int>(Enum.GetNames(typeof(EnumCompletedLessonAnswerType)).Length);

    [Required]
    public Guid CardsCollectionId { get; set; }
    [Required]
    public CardCollectionListModel CardCollection { get; set; } = null!;
    [Required]
    public string UserId { get; set; } = null!;
    
}