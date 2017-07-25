using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Helpers.Domain;

namespace Tennis.Helpers.Factory
{
    public interface IFactory<T>
        where T : class
    {
        T Construct();
    }
}
