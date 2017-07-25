using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Events;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Entities
{
    public partial class Set : Entity, IAggregateRoot
    {
        public IList<Game> Games { get; set; }

        private Set()
        {
            Id = Guid.NewGuid();
            Games = new List<Game>();
        }

        public static SetFactory Factory
        {
            get
            {
                return new SetFactory();
            }
        }

        public int Number { get; private set; }

        public SetScoreState SetState { get; set; }

        public void Start(IList<Player> players)
        {
            if (this != null)
            {
                DomainEvents.Raise(new SetStarted()
                {
                    Set = this,
                    Players = players
                });
            }
        }

        public void InitalizeSetScore()
        {
            SetState = new SetScoreStandard(this);
        }

        public void StartNexGame(IList<Player> players)
        {
            var game = Game.Factory.Players(players).Construct();
            Games.Add(game);
            game.Start(this);
        }
    }
}
