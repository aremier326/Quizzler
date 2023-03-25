using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzler.Bll.Interfaces.Entities
{
    public class ErrorModel
    {
        public IEnumerable<ErrorDetail> ErrorDetails { get; set; } = Array.Empty<ErrorDetail>();

        public bool IsSuccess
        {
            get
            {
                if (ErrorDetails != null && ErrorDetails.Any())
                {
                    return false;
                }
                return true;
            }
        }

        public static ErrorModel CreateSuccess() => new();
    }
}
