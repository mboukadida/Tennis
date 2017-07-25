using System;
using Tennis.Helpers.Domain;
using Tennis.Helpers.Factory;

namespace Tennis.DomainLayer.Entities
{
    public partial class Player : Entity
    {
        public class PlayerFactory : Factory<Player>
        {
            private readonly Player _player;

            public PlayerFactory()
            {
                _player = new Player();
            }

            public override Player Construct()
            {
                return _player;
            }

            public PlayerFactory Name(string name)
            {
                _player.Name = name;
                return this;
            }
        }
    }
}
