using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Interfaces.Entities
{
    public class ErrorDetail
    {
        public string ErrorMessage { get; set; }

        public Exception? Exception { get; set; }
    }
}
