using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Events
{
    public class SetStartedHandler : IHandler<SetStarted>
    {
        public void Handle(SetStarted args)
        {
            args.Set.InitalizeSetScore();
            args.Set.StartNexGame(args.Players);
        }
    }
}
