using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemWatcher
    {
        void BeginWatch();
        void EndWatch();
    }
}
