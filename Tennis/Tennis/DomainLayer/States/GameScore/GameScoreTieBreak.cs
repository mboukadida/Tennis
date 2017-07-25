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
    public class GameScoreTieBreak : GameScoreState
    {
        private Player _player;

        public GameScoreTieBreak(Game game)
        {
            Game = game;
        }

        public GameScoreTieBreak(Game game, Set set)
            : this(game)
        {
            Set = set;
        }

        public GameScoreTieBreak(GameScoreState scoreState)
            : this(scoreState.Game, scoreState.Set)
        {
        }

        public override void WinPoint(Player player)
        {
            player.TieBreakScore++;
            _player = player;
            CheckGameScoreState();
        }

        private void CheckGameScoreState()
        {
            if (_player.TieBreakScore >= 6 && GetSetScoreDifferenceBetweenPlayers() >= 2)
            {
                _player.GameScore = Entities.GameScore.Win;
                _player.SetScore = Entities.SetScore.Win;

                DomainEvents.Raise<SetWinned>(new SetWinned()
                {
                    Set = Set,
                    SetScoreState = new SetScoreOver(Set)
                });

                return;
            }

            Game.ScoreState = new GameScoreTieBreak(this);
        }

        public int GetSetScoreDifferenceBetweenPlayers()
        {
            var player1 = Game.Players[0];
            var player2 = Game.Players[1];
            return Math.Abs(player1.TieBreakScore - player2.TieBreakScore);
        }
    }
}
