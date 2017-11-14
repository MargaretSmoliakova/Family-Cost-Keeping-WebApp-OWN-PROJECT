using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.Internal
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
