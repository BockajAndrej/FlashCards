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

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController(ICardFacade facade) : ControllerBase
    {
        
        // GET: api/Card
        [HttpGet]
        public async Task<IQueryable<CardDetailModel>> GetCard(
            [FromQuery] string? strFilter,
            [FromQuery] string? strSortBy,
            [FromQuery] bool sortDesc = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            Expression<Func<CardEntity, bool>>? filter = null;
            if (!string.IsNullOrEmpty(strFilter))
              filter = l => l.Otazka == strFilter;
            Func<IQueryable<CardEntity>, IOrderedQueryable<CardEntity>>? orderBy = null;
            if (!string.IsNullOrEmpty(strSortBy))
                orderBy = l => l.OrderBy(s => s.Otazka);

            switch (strSortBy)
            {
                case "Otazka":
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Otazka) 
                        : l => l.OrderByDescending(s => s.Otazka);
                    break;
                case "SpravnaOdpoved":
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.SpravnaOdpoved) 
                        : l => l.OrderByDescending(s => s.SpravnaOdpoved);
                    break;
            }
            
            return await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
        }

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDetailModel>> GetCardEntity(Guid id)
        {
            var cardEntity = await facade.GetByIdAsync(id);
            return Ok(cardEntity);
        }

        // PUT: api/Card/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardEntity(Guid id, CardDetailModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            
            await facade.SaveAsync(model);
            
            return NoContent();
        }

        // POST: api/Card
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardDetailModel>> PostCardEntity(CardDetailModel cardEntity)
        {
            cardEntity.Id = Guid.Empty;
            var result = await facade.SaveAsync(cardEntity);
            return Ok(result);
        }

        // DELETE: api/Card/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardEntity(Guid id)
        {
            var result = await facade.DeleteAsync(id);
            if(result)
                return NoContent();
            return NotFound();
        }
    }
}
