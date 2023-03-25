using Quizzler.Dal.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Interfaces.Interfaces
{
    public interface ITestService
    {
        ValueTask<Test> Get(int id);

        ValueTask<ActiveTest> CheckTest(int testId);
    }
}
