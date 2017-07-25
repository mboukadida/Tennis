using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;

namespace Tennis.DomainLayer.States.GameScore
{
    public class GameScoreDeuce : GameScoreState
    {
        private IDictionary<AdvantageWinLose, Player> _players = new Dictionary<AdvantageWinLose, Player>();

        public GameScoreDeuce(Game game)
        {
            Game = game;
        }

        public GameScoreDeuce(GameScoreState scoreState)
            : this(scoreState.Game)
        {
        }

        public override void WinPoint(Player player)
        {
            _players.Add(AdvantageWinLose.Win, player);

            var opposingPlayer = Game.Players.Single(p => !ReferenceEquals(p, player));
            _players.Add(AdvantageWinLose.Lose, opposingPlayer);

            player.Advantage = true;
            _players[AdvantageWinLose.Lose].Advantage = false;

            CheckGameScoreState();
        }

        private void CheckGameScoreState()
        {
            Game.ScoreState = new GameScoreAdvantage(this);
        }
    }
}
