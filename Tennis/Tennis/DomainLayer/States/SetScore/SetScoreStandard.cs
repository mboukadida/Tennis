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
    public class SetScoreStandard : SetScoreState
    {
        private Game _game;
        private Player _player;

        public SetScoreStandard(Set set)
        {
            Set = set;
        }

        public SetScoreStandard(SetScoreState scoreState)
            : this(scoreState.Set)
        {
        }

        public override void WinGame(Game game, Player player)
        {
            player.SetScore++;
            _player = player;
            _game = game;
            CheckSetScoreState();
        }

        private void CheckSetScoreState()
        {
            switch (_player.SetScore)
            {
                case Entities.SetScore.Six:
                    {
                        if (GetSetScoreDifferenceBetweenPlayers() >= 2)
                        {
                            SetWinned();
                        }
                        else if (GetSetScoreDifferenceBetweenPlayers() == 1)
                        {
                            Set.SetState = new SetScoreStandard(this);
                        }
                        else if (GetSetScoreDifferenceBetweenPlayers() == 0)
                        {
                            Set.SetState = new SetScoreTieBreak(this);
                        }
                    }
                    break;
                case Entities.SetScore.Seven:
                    {
                        SetWinned();
                    }
                    break;
                default:
                    Set.SetState = new SetScoreStandard(this);
                    break;
            }
        }

        private void SetWinned()
        {
            _player.SetScore = Entities.SetScore.Win;
            DomainEvents.Raise<SetWinned>(new SetWinned()
            {
                Set = Set,
                SetScoreState = this
            });
        }

        private int GetSetScoreDifferenceBetweenPlayers()
        {
            var player1 = _game.Players[0];
            var player2 = _game.Players[1];
            return Math.Abs(player1.SetScore - player2.SetScore);
        }
    }
}
