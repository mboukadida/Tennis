using System;
using NUnit.Framework;
using Tennis.DomainLayer;
using Tennis.DomainLayer.Entities;
using System.Collections.Generic;
using NFluent;
using Tennis.DomainLayer.Events;
using System.Linq;
using Tennis.Helpers.Domain;
using Castle.Windsor;
using Tennis.DomainLayer.States.SetScore;
using Tennis.DomainLayer.States.GameScore;
using Tennis.Tests.Container;

namespace Tennis.Tests
{
    [TestFixture]
    public class TennisTests
    {
        IWindsorContainer _container;
        Player _player1;
        Player _player2;

        [OneTimeSetUp()]
        public void InitalizeWIndsorContainer()
        {
            _container = Initializer.RegisterTypes();
            DomainEvents.Container = _container;

            _player1 = Player.Factory.Name("Player 1").Construct();
            _player2 = Player.Factory.Name("Player 2").Construct();
        }

        #region Game Tests
        [Test]
        public void Should_Create_A_Game_With_Two_Players()
        {
            var playerList = ConstructTwoPlayer();

            Game game = Game.Factory
                .Players(playerList)
                .Construct();
            Check.That(game).IsNotNull();
            Check.That(game.Players).HasSize(2);
        }

        [Test]
        public void Should_Initialize_Players_Point_When_A_Game_Is_Started()
        {
            Game gameToCheck = null;
            DomainEvents.Register<GameStarted>(p => gameToCheck = p.Game);

            var playerList = ConstructTwoPlayer();

            Game game = Game.Factory
                .Players(playerList)
                .Construct();
            game.Start(null);

            Check.That(game).IsEqualTo(gameToCheck);
            Check.That(gameToCheck.Players[0].GameScore).Equals(GameScore.Love);
            Check.That(gameToCheck.Players[1].GameScore).Equals(GameScore.Love);
        }

        [Test]
        public void Should_Win_A_Game_When_A_Player_Win_4_Point()
        {
            Game gameToCheck = null;
            DomainEvents.Register<GameStarted>(p => gameToCheck = p.Game);
            DomainEvents.Register<PointWinned>(p => gameToCheck = p.Game);
            DomainEvents.Register<GameWinned>(p => gameToCheck = p.Game);

            var game = ConstructAGameWithTwoPlayers();
            game.Start(null);

            Player player1 = game.Players[0];
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);

            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreOver));
            Check.That(game.Players[0].GameScore).IsEqualTo(GameScore.Win);
        }

        [Test]
        public void Should_Reach_To_Deuce_When_A_Player_Win_A_Point_After_A_Score_40_To_40()
        {
            Game gameToCheck = null;
            DomainEvents.Register<GameStarted>(p => gameToCheck = p.Game);
            DomainEvents.Register<PointWinned>(p => gameToCheck = p.Game);
            DomainEvents.Register<GameWinned>(p => gameToCheck = p.Game);

            var game = ConstructAGameWithTwoPlayers();
            game.Start(null);

            Player player1 = game.Players[0];
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);

            Player player2 = game.Players[1];
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);

            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreDeuce));
            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreDeuce));
        }

        [Test]
        public void Should_Player_Win_A_Game_After_A_Deuce_And_Advantage()
        {
            Game gameToCheck = null;
            DomainEvents.Register<GameStarted>(p => gameToCheck = p.Game);
            DomainEvents.Register<PointWinned>(p => gameToCheck = p.Game);
            DomainEvents.Register<GameWinned>(p => gameToCheck = p.Game);

            var game = ConstructAGameWithTwoPlayers();
            game.Start(null);

            Player player1 = game.Players[0];
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);

            Player player2 = game.Players[1];
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);

            player2.WinPoint(null, game);
            player2.WinPoint(null, game);

            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreOver));
            Check.That(gameToCheck.Players.FirstOrDefault(p => p == player2).GameScore).IsEqualTo(GameScore.Win);
        }

        [Test]
        public void Should_Have_A_Deuce_Score_After_Player_Opponent_Advantage_Score()
        {
            Game gameToCheck = null;
            DomainEvents.Register<GameStarted>(p => gameToCheck = p.Game);
            DomainEvents.Register<PointWinned>(p => gameToCheck = p.Game);
            DomainEvents.Register<GameWinned>(p => gameToCheck = p.Game);

            var game = ConstructAGameWithTwoPlayers();
            game.Start(null);

            Player player1 = game.Players[0];
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);
            player1.WinPoint(null, game);

            Player player2 = game.Players[1];
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);
            player2.WinPoint(null, game);

            player2.WinPoint(null, game);
            player1.WinPoint(null, game);

            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreDeuce));
            Check.That(gameToCheck.ScoreState.GetType()).Equals(typeof(GameScoreDeuce));
        }
        #endregion

        #region Set Tests
        [Test]
        public void Should_Create_A_Set_With_A_Game_And_Two_Players_And_Start_It()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
                .Construct();

            set.Start(players);

            Check.That(set).IsNotNull();
            Check.That(set.Games).HasSize(1);
            Check.That(set.Games[0].Players).HasSize(2);

            var gameToCheck = set.Games[0];
            Check.That(gameToCheck.Players[0].GameScore).Equals(GameScore.Love);
            Check.That(gameToCheck.Players[1].GameScore).Equals(GameScore.Love);
            Check.That(gameToCheck.Players[0].SetScore).Equals(SetScore.Zero);
            Check.That(gameToCheck.Players[1].SetScore).Equals(SetScore.Zero);
        }

        [Test]
        public void Should_Increase_SetScore_After_A_Game_Is_Winned_And_Start_A_New_Game()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);

            var game1 = set.Games[0];
            var player1 = game1.Players[0];

            player1.WinPoint(set, game1);
            player1.WinPoint(set, game1);
            player1.WinPoint(set, game1);
            player1.WinPoint(set, game1);

            Check.That(game1.ScoreState.GetType()).Equals(typeof(GameScoreOver));
            Check.That(player1.GameScore).IsEqualTo(GameScore.Win);

            Check.That(set.SetState.GetType()).Equals(typeof(SetScoreStandard));
            Check.That(player1.SetScore).IsEqualTo(SetScore.One);

            Check.That(set.Games).HasSize(2);
            var game2 = set.Games[1];
            Check.That(game2.Players[0].SetScore).IsEqualTo(SetScore.One);
            Check.That(game2.Players[0].GameScore).IsEqualTo(GameScore.Love);
            Check.That(game2.Players[1].GameScore).IsEqualTo(GameScore.Love);
        }

        [Test]
        public void Should_Win_A_Set_After_Six_Games_Winned()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);
            GameWinnedByPlayer(set, set.Games[0], set.Games[0].Players[0]);
            GameWinnedByPlayer(set, set.Games[1], set.Games[1].Players[0]);
            GameWinnedByPlayer(set, set.Games[2], set.Games[2].Players[0]);
            GameWinnedByPlayer(set, set.Games[3], set.Games[3].Players[0]);
            GameWinnedByPlayer(set, set.Games[4], set.Games[4].Players[0]);
            GameWinnedByPlayer(set, set.Games[5], set.Games[5].Players[0]);

            Check.That(set.SetState.GetType()).Equals(typeof(SetScoreOver));
        }

        [Test]
        public void Should_Add_A_Game_After_Five_To_Five_Game_Score()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);

            GameWinnedByPlayer(set, set.Games[0], set.Games[0].Players[0]);
            GameWinnedByPlayer(set, set.Games[1], set.Games[1].Players[0]);
            GameWinnedByPlayer(set, set.Games[2], set.Games[2].Players[0]);
            GameWinnedByPlayer(set, set.Games[3], set.Games[3].Players[0]);
            GameWinnedByPlayer(set, set.Games[4], set.Games[4].Players[0]);

            GameWinnedByPlayer(set, set.Games[5], set.Games[5].Players[1]);
            GameWinnedByPlayer(set, set.Games[6], set.Games[6].Players[1]);
            GameWinnedByPlayer(set, set.Games[7], set.Games[7].Players[1]);
            GameWinnedByPlayer(set, set.Games[8], set.Games[8].Players[1]);
            GameWinnedByPlayer(set, set.Games[9], set.Games[9].Players[1]);

            GameWinnedByPlayer(set, set.Games[10], set.Games[10].Players[0]);

            Check.That(set.Games).HasSize(12);
        }

        [Test]
        public void Should_Win_A_Set_After_Seven_To_Five_Game_Score()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);

            GameWinnedByPlayer(set, set.Games[0], set.Games[0].Players[0]);
            GameWinnedByPlayer(set, set.Games[1], set.Games[1].Players[0]);
            GameWinnedByPlayer(set, set.Games[2], set.Games[2].Players[0]);
            GameWinnedByPlayer(set, set.Games[3], set.Games[3].Players[0]);
            GameWinnedByPlayer(set, set.Games[4], set.Games[4].Players[0]);

            GameWinnedByPlayer(set, set.Games[5], set.Games[5].Players[1]);
            GameWinnedByPlayer(set, set.Games[6], set.Games[6].Players[1]);
            GameWinnedByPlayer(set, set.Games[7], set.Games[7].Players[1]);
            GameWinnedByPlayer(set, set.Games[8], set.Games[8].Players[1]);
            GameWinnedByPlayer(set, set.Games[9], set.Games[9].Players[1]);

            GameWinnedByPlayer(set, set.Games[10], set.Games[10].Players[0]);
            GameWinnedByPlayer(set, set.Games[11], set.Games[11].Players[0]);

            Check.That(set.SetState.GetType()).Equals(typeof(SetScoreOver));
        }

        [Test]
        public void Should_Have_Tie_Break_Set_After_Six_To_Six_Game_Score()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);

            GameWinnedByPlayer(set, set.Games[0], set.Games[0].Players[0]);
            GameWinnedByPlayer(set, set.Games[1], set.Games[1].Players[0]);
            GameWinnedByPlayer(set, set.Games[2], set.Games[2].Players[0]);
            GameWinnedByPlayer(set, set.Games[3], set.Games[3].Players[0]);
            GameWinnedByPlayer(set, set.Games[4], set.Games[4].Players[0]);

            GameWinnedByPlayer(set, set.Games[5], set.Games[5].Players[1]);
            GameWinnedByPlayer(set, set.Games[6], set.Games[6].Players[1]);
            GameWinnedByPlayer(set, set.Games[7], set.Games[7].Players[1]);
            GameWinnedByPlayer(set, set.Games[8], set.Games[8].Players[1]);
            GameWinnedByPlayer(set, set.Games[9], set.Games[9].Players[1]);

            GameWinnedByPlayer(set, set.Games[10], set.Games[10].Players[0]);
            GameWinnedByPlayer(set, set.Games[11], set.Games[11].Players[1]);

            Check.That(set.SetState.GetType()).Equals(typeof(SetScoreTieBreak));
        }

        [Test]
        public void Should_Win_Set_After_Tie_Break_Game_Winned()
        {
            var players = ConstructTwoPlayer();
            var set = Set.Factory
               .Construct();
            set.Start(players);

            GameWinnedByPlayer(set, set.Games[0], set.Games[0].Players[0]);
            GameWinnedByPlayer(set, set.Games[1], set.Games[1].Players[0]);
            GameWinnedByPlayer(set, set.Games[2], set.Games[2].Players[0]);
            GameWinnedByPlayer(set, set.Games[3], set.Games[3].Players[0]);
            GameWinnedByPlayer(set, set.Games[4], set.Games[4].Players[0]);

            GameWinnedByPlayer(set, set.Games[5], set.Games[5].Players[1]);
            GameWinnedByPlayer(set, set.Games[6], set.Games[6].Players[1]);
            GameWinnedByPlayer(set, set.Games[7], set.Games[7].Players[1]);
            GameWinnedByPlayer(set, set.Games[8], set.Games[8].Players[1]);
            GameWinnedByPlayer(set, set.Games[9], set.Games[9].Players[1]);

            GameWinnedByPlayer(set, set.Games[10], set.Games[10].Players[0]);
            GameWinnedByPlayer(set, set.Games[11], set.Games[11].Players[1]);

            set.Games[12].Players[0].WinPoint(set, set.Games[12]);
            set.Games[12].Players[0].WinPoint(set, set.Games[12]);
            set.Games[12].Players[0].WinPoint(set, set.Games[12]);
            set.Games[12].Players[0].WinPoint(set, set.Games[12]);
            set.Games[12].Players[0].WinPoint(set, set.Games[12]);
            set.Games[12].Players[0].WinPoint(set, set.Games[12]);

            Check.That(set.SetState.GetType()).Equals(typeof(SetScoreOver));
        }

        #endregion

        #region Private Methods
        private static void GameWinnedByPlayer(Set set, Game game, Player player)
        {
            player.WinPoint(set, game);
            player.WinPoint(set, game);
            player.WinPoint(set, game);
            player.WinPoint(set, game);
        }

        private IList<Player> ConstructTwoPlayer()
        {
            var playerList = new List<Player>
            {
                (Player)_player1.Clone(),
                (Player)_player2.Clone()
            };
            return playerList;
        }

        private Game ConstructAGameWithTwoPlayers()
        {
            var players = ConstructTwoPlayer();
            Game game = Game.Factory
                .Players(players)
                .Construct();
            
            return game;
        }
        #endregion
    }
}
