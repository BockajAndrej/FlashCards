using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedLessonController(ICompletedLessonFacade facade)
        : ControllerBase<CompletedLessonEntity, CompletedLessonListModel, CompletedLessonDetailModel>(facade)
    {
        public override async Task<ActionResult<IEnumerable<CompletedLessonListModel>>> GetCard(
            string? strFilterAtrib, 
            string? strFilter,
            string? strSortBy, 
            bool sortDesc = false, 
            int pageNumber = 1,
            int pageSize = 10)
        {
            Expression<Func<CompletedLessonEntity, bool>>? filter = null;
            if (!string.IsNullOrEmpty(strFilter))
            {
                if (!string.IsNullOrEmpty(strFilter))
                    filter = l => l.CardCollection.Title.ToLower().Contains(strFilter.ToLower());
            }
            
            Func<IQueryable<CompletedLessonEntity>, IOrderedQueryable<CompletedLessonEntity>>? orderBy = null;
            switch (strSortBy)
            {
                case nameof(CompletedLessonEntity.CardCollection.Title):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.CardCollection.Title) 
                        : l => l.OrderByDescending(s => s.CardCollection.Title);
                    break;
            }
            
            var result = await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
            return Ok(result.ToList());
        }
        
        [HttpPost]
        [Authorize]
        public override async Task<ActionResult<CompletedLessonDetailModel>> PostCardEntity(CompletedLessonDetailModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId ?? throw new UnauthorizedAccessException();
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}