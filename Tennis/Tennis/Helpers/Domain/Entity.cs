using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Helpers.Domain
{
   public abstract class Entity
    {
        public virtual Guid Id { get; set; }
    }
}
