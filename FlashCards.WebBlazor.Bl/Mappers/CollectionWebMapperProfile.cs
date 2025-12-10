using AutoMapper;
using FlashCards.WebBlazor.Bl.ApiClient;

namespace FlashCards.WebBlazor.Bl.Mappers;

public class CollectionWebMapperProfile : Profile
{
    public CollectionWebMapperProfile()
    {
        CreateMap<CollectionDetailModel, CollectionListModel>().ReverseMap();
    }
}