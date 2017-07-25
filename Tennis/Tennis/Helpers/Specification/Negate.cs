using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Helpers.Specification
{
    public class Negated<T> : SpecificationBase<T>
    {
        private readonly ISpecification<T> _inner;

        public Negated(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public override Expression<Func<T, bool>> Specification
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(this._inner.Specification, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
