using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.Events;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.States.GameScore
{
    public class GameScoreStandard : GameScoreState
    {
        private Player _player;

        public GameScoreStandard(Game game)
        {
            Game = game;
        }

        public GameScoreStandard(Game game, Set set)
            : this(game)
        {
            Set = set;
        }

        public GameScoreStandard(GameScoreState scoreState)
            : this(scoreState.Game)
        {
        }

        public override void WinPoint(Player player)
        {
            player.GameScore++;
            _player = player;
            CheckGameScoreState();
        }

        private void CheckGameScoreState()
        {
            if (Game.Players.Any(x => x.GameScore == Entities.GameScore.Win))
            {
                DomainEvents.Raise<GameWinned>(new GameWinned()
                {
                    Set = Set,
                    Player = _player,
                    Game = Game,
                    GameScoreState = this
                });

                return;
            }

            var player1 = Game.Players[0];
            var player2 = Game.Players[1];

            if (player1.GameScore == player2.GameScore && player1.GameScore == Entities.GameScore.Fourty)
            {
                Game.ScoreState = new GameScoreDeuce(this);
            }
        }
    }
}
