using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;

namespace FlashCards.Api.Bl.Facades;

public class CardFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<CardEntity, CardDetailModel>(dbContext, mapper)
        , ICardFacade
{

}