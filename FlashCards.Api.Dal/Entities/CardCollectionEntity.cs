using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public class CardCollectionEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Nazev { get; set; } = null!;
    [Required]
    public DateTime ZacatekProAkceptovaniOdpovediDateTime { get; set; }
    [Required]
    public DateTime KonecProAkceptovaniOdpovediDateTime { get; set; }
}