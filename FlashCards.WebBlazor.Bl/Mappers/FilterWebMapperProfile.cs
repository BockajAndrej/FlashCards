using AutoMapper;
using FlashCards.WebBlazor.Bl.ApiClient;

namespace FlashCards.WebBlazor.Bl.Mappers;

public class FilterWebMapperProfile : Profile
{
    public FilterWebMapperProfile()
    {
        CreateMap<FilterDetailModel, FilterListModel>().ReverseMap();
    }
}