﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Model.Api
{
    public class Account : IDisposable
    {

        public string currency;
        public double balance;
        public double locked;
        public double avg_buy_price;
        public bool avg_buy_price_modified;
        public string unit_currency;

        public void Dispose() { } // using{} 키워드를 사용하기 위함 
    }
}
