using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;

namespace Tennis.DomainLayer.States.SetScore
{
    public class SetScoreTieBreak : SetScoreState
    {
        public SetScoreTieBreak(Set set)
        {
            Set = set;
        }

        public SetScoreTieBreak(SetScoreState scoreState)
            : this(scoreState.Set)
        {
        }

        public override void WinGame(Game game, Player player)
        {
        }
    }
}
