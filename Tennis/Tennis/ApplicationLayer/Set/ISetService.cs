using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.ApplicationLayer.Set
{
    public interface ISetService
    {
        SetDto Start(SetDto dtoSet);
        string DisplayScore(SetDto dtoSet);
    }
}
