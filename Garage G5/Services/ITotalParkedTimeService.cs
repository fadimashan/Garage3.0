using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Services
{
    public interface ITotalParkedTimeService
    {
        Task<IEnumerable<string>> GetTotalPakedTime();
    }
}
