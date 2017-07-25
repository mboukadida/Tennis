using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Helpers.Domain
{
    public interface IHandler<T>
        where T : IDomainEvent
    {
        void Handle(T args);
    }
}
