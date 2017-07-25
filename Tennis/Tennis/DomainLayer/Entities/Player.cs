using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Events;
using Tennis.DomainLayer.States.GameScore;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Entities
{
    public partial class Player : Entity, ICloneable
    {
        private Player()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }

        public GameScore GameScore { get; set; }

        public static PlayerFactory Factory
        {
            get { return new PlayerFactory(); }
        }

        public bool Advantage { get; internal set; }

        public SetScore SetScore { get; set; }

        public int TieBreakScore { get; set; }

        public void IntializeGameScore(Game game)
        {
           GameScore = GameScore.Love;
        }

        public void WinPoint(Set set, Game game)
        {
            DomainEvents.Raise<PointWinned>(new PointWinned()
            {
                Set = set,
                Game = game,
                Player = this
            });
        }

        public virtual object Clone()
        {
           return MemberwiseClone();
        }
    }
}
