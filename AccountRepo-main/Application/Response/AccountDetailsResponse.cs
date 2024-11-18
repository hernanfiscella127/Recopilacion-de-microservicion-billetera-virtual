using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class AccountDetailsResponse
    {
        public AccountResponse Account {  get; set; }
        public UserResponse User { get; set; }
        public List<TransferResponse> Transfers { get; set; }
    }
}
