using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TransferStatus
    {
        public int TransferStatusId {  get; set; }
        public string Status { get; set; }

        public List<Transfer>Transfers { get; set; }
    }
}
