using System;
using System.Collections.Generic;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;
using Tennis.Helpers.Factory;

namespace Tennis.DomainLayer.Entities
{
    public partial class Game : Entity, IAggregateRoot
    {
        public class GameFactory : Factory<Game>
        {
            private readonly Game _game;

            public GameFactory()
            {
                _game = new Game();
            }
            
            public override Game Construct()
            {
                return _game;
            }

            public GameFactory Players(IList<Player> playerList)
            {
                _game.Players = playerList;
                return this;
            }
        }
    }
 }