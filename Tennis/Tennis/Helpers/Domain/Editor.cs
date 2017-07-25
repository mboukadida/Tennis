using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Helpers.Domain;

namespace Tennis.Helpers.Domain
{
    public abstract class Editor<T> : IEditor<T>
        where T : Entity, ICloneable
    {
        protected virtual T EntityToEdit { get; set; }
        protected virtual T EntityEdited { get; set; }

        public Editor(T entity)
        {
            if (entity == null)
                return;

            EntityToEdit = entity;
            EntityEdited = entity.Clone() as T;
        }

        public abstract void ValidateModifications();        
    }
}
