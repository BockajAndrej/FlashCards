using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController(ICardFacade facade) : ControllerBase<CardEntity, CardListModel, CardDetailModel>(facade)
    {
        
        // GET: api/Card
        [HttpGet]
        public override async Task<IQueryable<CardListModel>> GetCard(
            [FromQuery] string? strFilterAtrib,
            [FromQuery] string? strFilter,
            [FromQuery] string? strSortBy,
            [FromQuery] bool sortDesc = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            Expression<Func<CardEntity, bool>> filter = l => true;
            switch (strFilterAtrib)
            {
                case nameof(CardEntity.Otazka):
                    filter = l => l.Otazka == strFilter;
                    break;
                case nameof(CardEntity.Popis):
                    filter = l => l.Popis == strFilter;
                    break;
            }
            
            Func<IQueryable<CardEntity>, IOrderedQueryable<CardEntity>>? orderBy = null;
            switch (strSortBy)
            {
                case nameof(CardEntity.Otazka):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Otazka) 
                        : l => l.OrderByDescending(s => s.Otazka);
                    break;
                case nameof(CardEntity.Popis):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Popis) 
                        : l => l.OrderByDescending(s => s.Popis);
                    break;
            }
            
            return await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
        }
        
        public override async Task<ActionResult<CardDetailModel>> PostCardEntity(CardDetailModel model)
        {
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}
