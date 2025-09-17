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
              filter = l => l.Nazev == strFilter;
            
            
            Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>>? orderBy = null;
            switch (strSortBy)
            {
                case "Nazev":
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Nazev) 
                        : l => l.OrderByDescending(s => s.Nazev);
                    break;
            }
            
            return await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
        }
    }
}
