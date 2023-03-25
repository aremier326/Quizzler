using Quizzler.Dal.Interfaces.Entities;
using Quizzler.Dal.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Interfaces.Interfaces
{
    public interface IQuizService
    {
        ValueTask CreateAsync(Quiz quiz);

        ValueTask UpdateAsync(Quiz quiz);

        ValueTask DeleteAsync(Quiz quiz);

        ValueTask<IEnumerable<Test>> GetTests(TestTypeEnum category, int number = 5);

        //ValueTask<int> GetResult(Quiz quiz);

    }
}
