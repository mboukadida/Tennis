using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.States.GameScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class GameWinned : IDomainEvent
    {
        public Game Game { get; internal set; }
        public GameScoreState GameScoreState { get; internal set; }
        public Set Set {get; internal set;}
        public Player Player { get; internal set; }
    }
}
