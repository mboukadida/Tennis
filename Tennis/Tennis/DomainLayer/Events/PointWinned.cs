using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class PointWinned : IDomainEvent
    {
        public Game Game { get; set; }

        public Player Player { get; set; }

        public Set Set { get; set; }
    }
}
