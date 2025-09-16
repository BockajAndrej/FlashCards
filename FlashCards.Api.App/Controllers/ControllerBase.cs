using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Common.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

public abstract class ControllerBase<TEntity, TModel>(IFacade<TEntity, TModel> facade) : Controller where TModel : IEntity
{
    // GET: api/Card
    [HttpGet]
    public abstract Task<IQueryable<TModel>> GetCard(
        [FromQuery] string? strFilter,
        [FromQuery] string? strSortBy,
        [FromQuery] bool sortDesc = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10);

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetCardEntity(Guid id)
        {
            var cardEntity = await facade.GetByIdAsync(id);
            return Ok(cardEntity);
        }

        // PUT: api/Card/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardEntity(Guid id, TModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            
            await facade.SaveAsync(model);
            
            return NoContent();
        }

        // POST: api/Card
        [HttpPost]
        public virtual async Task<ActionResult<TModel>> PostCardEntity(TModel model)
        {
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
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