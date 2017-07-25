using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.States.GameScore;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class GameWinnedHandler : IHandler<GameWinned>
    {
        public void Handle(GameWinned args)
        {
            args.Game.ScoreState = new GameScoreOver(args.GameScoreState);
            if(args.Set != null)
            {
                args.Set.SetState.WinGame(args.Game, args.Player);
                if (args.Set.SetState.GetType().Equals(typeof(SetScoreOver)))
                    return;

                args.Set.StartNexGame(args.Game.Players.Select(x => (Player)x.Clone()).ToList());
            }
        }
    }
}
