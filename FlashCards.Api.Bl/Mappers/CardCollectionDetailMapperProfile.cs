using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;

namespace FlashCards.Api.Bl.Mappers;

public class CardCollectionDetailMapperProfile : Profile
{
    public CardCollectionDetailMapperProfile()
    {
        CreateMap<CardCollectionEntity, CardCollectionDetailModel>();
        CreateMap<CardCollectionDetailModel, CardCollectionEntity>();
    }
}