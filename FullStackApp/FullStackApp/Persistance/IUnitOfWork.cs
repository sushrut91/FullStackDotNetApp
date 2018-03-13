using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackApp.Persistance
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}
