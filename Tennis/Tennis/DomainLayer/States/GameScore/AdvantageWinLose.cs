using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.States.GameScore
{
    public enum AdvantageWinLose
    { 
        Lose,
        Win
    }
}
