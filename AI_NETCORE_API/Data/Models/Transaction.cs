﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("Transactions")]
    public partial class Transaction
    {
        public int Id { get; set; }
        public int SellOfferId { get; set; }
        public int BuyOfferId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public BuyOffer BuyOffer { get; set; }
        public SellOffer SellOffer { get; set; }
    }
}