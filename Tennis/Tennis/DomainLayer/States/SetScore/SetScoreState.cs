using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;

namespace Tennis.DomainLayer.States.SetScore
{
    public abstract class SetScoreState
    {
        public Set Set { get; set; }

        public abstract void WinGame(Game game, Player player);
    }
}
