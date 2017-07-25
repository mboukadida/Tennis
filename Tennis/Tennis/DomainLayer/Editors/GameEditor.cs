using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.DomainLayer.Entities;
using Tennis.DomainLayer.States.GameScore;
using Tennis.Helpers.Domain;

namespace Tennis.DomainLayer.Editors
{
    public class GameEditor : Editor<Game>
    {
        public GameEditor(Game entity) 
            : base(entity)
        {
        }

        public GameEditor ScoreState(GameScoreState scoreState)
        {
            EntityEdited.ScoreState = scoreState;
            return this;
        }

        public override void ValidateModifications()
        {
            EntityToEdit.ScoreState = EntityEdited.ScoreState;
        }
    }
}
