using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;

namespace FlashCards.Api.Bl.Facades;

public class CompletedLessonFacade(FlashCardsDbContext dbContext, IMapper mapper) 
    : FacadeBase<CompletedLessonEntity, CompletedLessonDetailModel>(dbContext, mapper),
        ICompletedLessonFacade
{
    
}