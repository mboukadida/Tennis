using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.Helpers.Domain;
using Tennis.Helpers.Extensions;

namespace Tennis.DomainLayer.Events
{
    public class PointWinnedHadler : IHandler<PointWinned>
    {
        public void Handle(PointWinned args)
        {
            var game = args.Game;
            var player = args.Player;

            game.WinPoint(args.Set, player); 
        }
    }
}
