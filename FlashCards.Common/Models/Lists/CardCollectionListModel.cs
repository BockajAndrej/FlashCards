using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CardCollectionListModel : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Nazev { get; set; } = null!;
    [Required]
    public DateTime ZacatekProAkceptovaniOdpovediDateTime { get; set; }
    [Required]
    public DateTime KonecProAkceptovaniOdpovediDateTime { get; set; }
}