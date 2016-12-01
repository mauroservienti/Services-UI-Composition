﻿using Marketing.Data.Models;
using Marketing.ProductDrafts.Events;
using NServiceBus;
using Shipping.ShippingDetails.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.StockItems.Events;
using Marketing.API.Host.Commands;

namespace Marketing.API.Host.Sagas
{
    class ProductDraftSaga
        : Saga<ProductDraftSaga.State>,
        IAmStartedByMessages<IStockItemCreatedEvent>,
        IAmStartedByMessages<IShippingDetailsDefinedEvent>,
        IHandleMessages<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>
    {
        public class State : ContainSagaData
        {
            public string StockItemId { get; set; }
            public bool StockItemReady { get; set; }
            public string ShippingDetailsId { get; set; }
            public bool ShippingDetailsReady { get; set; }
            public string ProductDraftId { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<State> mapper)
        {
            mapper.ConfigureMapping<IStockItemCreatedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<IShippingDetailsDefinedEvent>(e => e.StockItemId).ToSaga(s => s.StockItemId);
            mapper.ConfigureMapping<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>(e => e.StockItemId).ToSaga(s => s.StockItemId);
        }

        public async Task Handle(IStockItemCreatedEvent message, IMessageHandlerContext context)
        {
            Data.StockItemId = message.StockItemId;
            Data.StockItemReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft(context).ConfigureAwait(false);
            }
        }

        public async Task Handle(IShippingDetailsDefinedEvent message, IMessageHandlerContext context)
        {
            Data.ShippingDetailsId = message.ShippingDetailsId;
            Data.ShippingDetailsReady = true;

            if (CanCreateProductDraft())
            {
                await CreateProductDraft(context).ConfigureAwait(false);
            }
        }

        public async Task Handle(HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand message, IMessageHandlerContext context)
        {
            await context.Publish<IProductDraftReadyToBePricedEvent>(e =>
            {
                e.ProductDraftId = Data.ProductDraftId;
                e.StockItemId = Data.StockItemId;
            })
            .ConfigureAwait(false);
        }

        bool CanCreateProductDraft()
        {
            return Data.StockItemReady && Data.ShippingDetailsReady;
        }

        async Task CreateProductDraft(IMessageHandlerContext context)
        {
            var session = context.SynchronizedStorageSession.RavenSession();

            var draft = new ProductDraft()
            {
                StockItemId = Data.StockItemId,
            };

            await session.StoreAsync(draft).ConfigureAwait(false);

            Data.ProductDraftId = draft.Id;

            await context.Publish<IProductDraftCreatedEvent>(e =>
            {
                e.ProductDraftId = draft.Id;
                e.StockItemId = draft.StockItemId;
            })
            .ConfigureAwait(false);
        }
    }
}