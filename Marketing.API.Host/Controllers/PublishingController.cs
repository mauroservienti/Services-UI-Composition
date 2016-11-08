﻿using Marketing.Data.Context;
using System.Web.Http;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/Publishing")]
    public class PublishingController : ApiController
    {
        private readonly IMarketingContext _context;

        public PublishingController(IMarketingContext marketingRepository)
        {
            _context = marketingRepository;
        }

        [HttpGet]
        public dynamic GetHomeShowcase()
        {
            return new
            {
                HeadlineStockItemId = 1,
                ShowcaseStockItemIds = new[] { 2, 4, 5 }
            };
        }
    }
}