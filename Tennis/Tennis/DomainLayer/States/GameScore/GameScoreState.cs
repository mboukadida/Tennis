using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;

namespace Tennis.DomainLayer.States.GameScore
{
    public abstract class GameScoreState
    {
        public Game Game { get; set; }

        public Set Set { get; set; }

        public abstract void WinPoint(Player player);
    }
}
