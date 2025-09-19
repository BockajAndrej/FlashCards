using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;

namespace FlashCards.Api.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardCollectionController(ICardCollectionFacade facade) : ControllerBase<CardCollectionEntity, CardCollectionListModel, CardCollectionDetailModel>(facade)
	{
		// GET: api/Card
		[HttpGet]
		public override async Task<IQueryable<CardCollectionListModel>> GetCard(
			[FromQuery] string? strFilterAtrib,
			[FromQuery] string? strFilter,
			[FromQuery] string? strSortBy,
			[FromQuery] bool sortDesc = false,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 10)
		{
			Expression<Func<CardCollectionEntity, bool>>? filter = null;
			if (!string.IsNullOrEmpty(strFilter))
				filter = l => l.Title.Contains(strFilter);
			//filter = l => l.Title == strFilter;


			Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>>? orderBy = null;
			switch (strSortBy)
			{
				case nameof(CardCollectionEntity.Title):
					orderBy = sortDesc
						? l => l.OrderBy(s => s.Title)
						: l => l.OrderByDescending(s => s.Title);
					break;
			}

			return await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
		}

		[HttpPost]
		[Authorize]
		public override async Task<ActionResult<CardCollectionDetailModel>> PostCardEntity(CardCollectionDetailModel model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			model.UserId = userId ?? throw new UnauthorizedAccessException();
			model.Id = Guid.Empty;
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}