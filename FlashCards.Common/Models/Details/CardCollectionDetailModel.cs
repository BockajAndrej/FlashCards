using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class CardCollectionDetailModel : CardCollectionListModel
{
    public ICollection<CardDetailModel> Cards { get; set; } = new List<CardDetailModel>();
}