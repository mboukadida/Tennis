using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;

namespace Tennis.DomainLayer.States.GameScore
{
    public class GameScoreOver : GameScoreState
    {
        public GameScoreOver(Game game)
        {
            Game = game;
        }

        public GameScoreOver(GameScoreState scoreState)
            : this(scoreState.Game)
        {
        }

        public override void WinPoint(Player player)
        {
        }
    }
}
