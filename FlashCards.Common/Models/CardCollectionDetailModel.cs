using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models;

public class CardCollectionDetailModel : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Nazev { get; set; } = null!;
    [Required]
    public DateTime ZacatekProAkceptovaniOdpovediDateTime { get; set; }
    [Required]
    public DateTime KonecProAkceptovaniOdpovediDateTime { get; set; }
    
    public ICollection<CardDetailModel> Cards { get; set; } = new List<CardDetailModel>();
}