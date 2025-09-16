using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;

namespace FlashCards.Common.Models;

public class CardDetailModel
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
}