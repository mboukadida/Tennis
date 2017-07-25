using System;
using System.Collections.Generic;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;
using Tennis.Helpers.Factory;

namespace Tennis.DomainLayer.Entities
{
    public partial class Set : Entity, IAggregateRoot
    {
        public class SetFactory : Factory<Set>
        {
            private readonly Set _set;

            public SetFactory()
            {
                _set = new Set();
            }

            public override Set Construct()
            {
                return _set;
            }

            public SetFactory Number(int setNumber)
            {
                _set.Number = setNumber;
                return this;
            }

            //public SetFactory Games(IList<Game> gameList)
            //{
            //    _set.Games = gameList;
            //    return this;
            //}

            //public SetFactory Players(IList<Player> players)
            //{
            //    _set.Games.Add(Game.Factory.Players(players).Construct());
            //    return this;
            //}
        }
    }
}
