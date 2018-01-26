using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Core
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
