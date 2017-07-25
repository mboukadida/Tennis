using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class SetStarted : IDomainEvent
    {
        public Set Set { get; set; }
        public IList<Player> Players { get; set; }
    }
}
