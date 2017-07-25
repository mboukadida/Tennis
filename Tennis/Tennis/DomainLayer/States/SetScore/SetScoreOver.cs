using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.States.SetScore
{
    public class SetScoreOver : SetScoreState
    {
        public SetScoreOver(Set set)
        {
            Set = set;
        }

        public SetScoreOver(SetScoreState scoreState)
            : this(scoreState.Set)
        {
        }

        public override void WinGame(Game game, Player player)
        {
            player.SetScore = Entities.SetScore.Win;
            DomainEvents.Raise<SetWinned>(new SetWinned()
            {
                Set = Set,
                SetScoreState = this
            });
        }
    }
}
