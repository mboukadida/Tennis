using System;
using System.Collections.Generic;
using System.Linq;
using Tennis.DomainLayer.Editors;
using Tennis.DomainLayer.Events;
using Tennis.DomainLayer.States.GameScore;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;
using Tennis.Helpers.Extensions;

namespace Tennis.DomainLayer.Entities
{
    public partial class Game : Entity, IAggregateRoot, ICloneable
    {
        public IList<Player> Players { get; private set; }

        public bool IsTieBreakGame(Set set)
        {
            return set != null && set.SetState.GetType().Equals(typeof(SetScoreTieBreak));
        }

        public void InitializePlayersGameScore(Set set)
        {
            foreach (var player in Players)
            {
                if (IsTieBreakGame(set))
                {
                    player.TieBreakScore = 0;
                }

                player.GameScore = GameScore.Love;
            }

            if (IsTieBreakGame(set))
            {
                ScoreState = new GameScoreTieBreak(this, set);
            }
            else
            {
                ScoreState = new GameScoreStandard(this, set);
            }
        }

        private Game()
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
        }

        public void Start(Set set)
        {
            if (this != null)
            {
                DomainEvents.Raise(new GameStarted() { Game = this, Set = set });
            }
        }

        public static GameFactory Factory
        {
            get { return new GameFactory(); }
        }

        public string DiplayScore()
        {
            throw new NotImplementedException();
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual GameEditor Edit
        {
            get
            {
                return new GameEditor(this);
            }
        }

        public GameScoreState ScoreState { get; set; }

        public void WinPoint(Set set, Player player)
        {
            ScoreState.WinPoint(player);
        }
    }
}