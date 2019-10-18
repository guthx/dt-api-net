﻿using Data.Providers.User.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Request.Concrete
{
    public class GetUserByIdRequest : IGetUserByIdRequest
    {
        public GetUserByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
