using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }

        public OperationResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
    public class OperationResult<TEntity> : OperationResult
    {
        public TEntity Entity { get; set; }

        public OperationResult(bool isSuccess):base(isSuccess)
        {

        }
    }
}
