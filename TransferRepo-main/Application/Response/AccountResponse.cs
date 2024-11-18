﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class AccountResponse
    {
        public Guid AccountId { get; set; }
        public string CBU { get; set; }
        public string Alias { get; set; }
        public string NumeroDeCuenta { get; set; }
        public decimal Balance { get; set; }
        public string TipoDeCuenta { get; set; }
        public string TipoDeMoneda { get; set; }
        public string EstadoDeLaCuenta { get; set; }
    }
}
