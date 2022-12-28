using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Dto
{
    public class TransferDTO
    {
        public long AccountNumberOrigin { get; set; }

        public long AccountNumberDestination { get; set; }

        public double Amount { get; set; }
    }
}
