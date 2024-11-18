using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class AccountBalanceRequest
    {
        public bool Option { get; set; } 
        public decimal Balance {  get; set; }
    }
}
