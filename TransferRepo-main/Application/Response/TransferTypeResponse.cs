using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class TransferTypeResponse
    {
        public int TransferTypeId { get; set; }
        public required string Name { get; set; }
    }
}
