using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class UserListModel : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Jmeno { get; set; } = null!;
    public string? Fotografie { get; set; }
    [Required]
    public EnumUserRole Role { get; set; }
}