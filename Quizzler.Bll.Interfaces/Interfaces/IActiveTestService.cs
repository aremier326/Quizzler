using Quizzler.Dal.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Interfaces.Interfaces
{
    public interface IActiveTestService
    {
        ValueTask CreateAsync(ActiveTest activeTest);

        ValueTask UpdateAsync(ActiveTest activeTest);

        ValueTask DeleteAsync(ActiveTest activeTest);
    }
}
