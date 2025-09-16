using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;

namespace FlashCards.Api.Bl.Mappers;

public class CardDetailMapperProfile : Profile
{
    public CardDetailMapperProfile()
    {
        CreateMap<CardEntity, CardDetailModel>();
        CreateMap<CardDetailModel, CardEntity>();
    }
}