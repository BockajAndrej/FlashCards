using System.Linq.Expressions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController(ICardFacade facade) : ControllerBase<CardEntity, CardListModel, CardDetailModel>(facade)
    {
        
        // GET: api/Card
        [HttpGet]
        [Authorize(Policy = "AdminRole")]
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
                case nameof(CardEntity.Question):
                    filter = l => l.Question == strFilter;
                    break;
                case nameof(CardEntity.Description):
                    filter = l => l.Description == strFilter;
                    break;
            }
            
            Func<IQueryable<CardEntity>, IOrderedQueryable<CardEntity>>? orderBy = null;
            switch (strSortBy)
            {
                case nameof(CardEntity.Question):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Question) 
                        : l => l.OrderByDescending(s => s.Question);
                    break;
                case nameof(CardEntity.Description):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Description) 
                        : l => l.OrderByDescending(s => s.Description);
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
