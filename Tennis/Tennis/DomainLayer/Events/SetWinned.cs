using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class SetWinned : IDomainEvent
    {
        public Set Set { get; internal set; }
        public SetScoreState SetScoreState { get; set; }
    }
}
