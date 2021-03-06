﻿using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.BuyOffers.Response.Concrete;
using Domain.Repositories.BuyOfferRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Repositories.BaseRepo.Response;
using Domain.Updaters.BuyOffers.Request.Abstract;
using Domain.Updaters.BuyOffers.Response.Abstract;
using Domain.Updaters.BuyOffers.Response.Concrete;

namespace Domain.Providers.BuyOffers.Concrete
{
    public class BuyOffersProvider : IBuyOffersProvider
    {
        private readonly ILogger _logger;
        private readonly IBuyOfferRepository _buyOffers;

        public BuyOffersProvider(ILogger logger, IBuyOfferRepository buyOffers)
        {
            _logger = logger;
            _buyOffers = buyOffers;
        }

        public IGetBuyOffersByUserIdResponse GetBuyOffersByUserId(IGetBuyOffersByUserIdRequest getBuyOffersByUserIdRequest)
        {
            try
            {
                var result = _buyOffers.GetBuyOffersByUserId(getBuyOffersByUserIdRequest.UserId);
                return new GetBuyOffersByUserIdResponse(result.Object.ToList(), result.DatabaseTime);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOffersByUserIdResponse();
            }
        }

       public IGetBuyOffersToStockExecutionResponse GetBuyOfferToStockExecution(IGetBuyOffersToStockExecutionRequest getBuyOffersToStockExecutionRequest)
        {
            try
            {
                RepositoryResponse<IEnumerable<BuyOffer>> buyOfferToStockExecute = _buyOffers.GetSellOfferToStockExecute(getBuyOffersToStockExecutionRequest.Quantity,getBuyOffersToStockExecutionRequest.CompanyId);
                return new GetBuyOffersToStockExecutionResponse(buyOfferToStockExecute.Object.ToList(), buyOfferToStockExecute.DatabaseTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOffersToStockExecutionResponse();
            }
        }
    }
}
