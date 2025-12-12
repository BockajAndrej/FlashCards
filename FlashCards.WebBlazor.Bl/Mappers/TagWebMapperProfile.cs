using AutoMapper;
using FlashCards.WebBlazor.Bl.ApiClient;

namespace FlashCards.WebBlazor.Bl.Mappers;

public class TagWebMapperProfile : Profile
{
    public TagWebMapperProfile()
    {
        CreateMap<TagDetailModel, TagListModel>().ReverseMap();
    }
}