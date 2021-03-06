﻿using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Response.BuyOffers
{
    public class GetBuyOffersByUserIdResponseModel
    {
        public IList<BuyOfferModel> BuyOffers { get; set; }
        public ExecutionDetails ExecDetails { get; set; }
    }
}
