using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class CardEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public EnumCardType TypOtazkyEnum { get; set; }
    [Required]
    public EnumCardType TypOdpovediEnum { get; set; }
    [Required]
    public string Otazka { get; set; } = null!;
    [Required]
    public string SpravnaOdpoved { get; set; } = null!;
    public string? DoplnujuciPopis { get; set; }
    
    [Required]
    public Guid CardCollectionId { get; set; }
    [Required]
    public CardCollectionEntity CardCollection { get; set; } = null!;
}