﻿using Domain.BusinessObject;
using Domain.Providers.Common.Abstract;
using Domain.Repositories.BaseRepo.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.BuyOffers.Response.Abstract
{
    public interface IGetBuyOffersByUserIdResponse : IProvideResult, IDatabaseExecutionTimeDetails
    {
        IList<BuyOffer> BuyOffer { get; }
    }
}
