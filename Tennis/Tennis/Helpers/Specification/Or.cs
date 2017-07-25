using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Helpers.Specification
{
    public class Or<T> : SpecificationBase<T>
    {
        ISpecification<T> left;
        ISpecification<T> right;

        public Or(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> Specification
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(left.Specification, objParam),
                        Expression.Invoke(right.Specification, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
