using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.States.GameScore
{
    public class GameScoreAdvantage : GameScoreState
    {
        private bool _gameWinned;
        private Player _player;
        
        public GameScoreAdvantage(Game game)
        {
            Game = game;
        }

        public GameScoreAdvantage(Set set, Game game)
            : this(game)
        {
            Set = set;
        }

        public GameScoreAdvantage(GameScoreState scoreState)
            : this(scoreState.Set, scoreState.Game )
        {
        }

        public override void WinPoint(Player player)
        {
            _player = player;
            if(player.Advantage)
            {
                player.GameScore = Entities.GameScore.Win;
                _gameWinned = true;
            }
            else
            {
                player.Advantage = false;

                var opposingPlayer = Game.Players.Single(p => !ReferenceEquals(p, player));
                opposingPlayer.Advantage = false;
            }

            CheckGameScoreState();
        }

        private void CheckGameScoreState()
        {
            if(_gameWinned)
            {
                DomainEvents.Raise<GameWinned>(new GameWinned()
                {
                    Set = Set,
                    Player = _player,
                    Game = Game,
                    GameScoreState = this
                });
            }
            else
            {
                Game.ScoreState = new GameScoreDeuce(this);
            }
        }
    }
}
