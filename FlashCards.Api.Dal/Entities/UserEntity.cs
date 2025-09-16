using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Jmeno { get; set; } = null!;
    public string? Fotografie { get; set; }
    [Required]
    public EnumUserRole Role { get; set; }
}