using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CardCollectionListModel : IEntityModel
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public DateTime StartTimeForAcceptedAnswers { get; set; }
    [Required]
    public DateTime EndTimeForAcceptedAnswers { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
}