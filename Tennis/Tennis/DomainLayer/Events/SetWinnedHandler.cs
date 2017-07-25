using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.States.SetScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class SetWinnedHandler : IHandler<SetWinned>
    {
        public void Handle(SetWinned args)
        {
            args.Set.SetState = new SetScoreOver(args.SetScoreState);
        }
    }
}
