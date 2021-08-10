using System;
using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Utilities;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    public class QueryableComparableGreaterThanHandler
        : QueryableComparableOperationHandler
    {
        public QueryableComparableGreaterThanHandler(
            ITypeConverter typeConverter,
            InputParser inputParser)
            : base(typeConverter, inputParser)
        {
            CanBeNull = false;
        }

        protected override int Operation => DefaultFilterOperations.GreaterThan;

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterOperationField field,
            IValueNode value,
            object? parsedValue)
        {
            Expression property = context.GetInstance();
            parsedValue = ParseValue(value, parsedValue, field.Type, context);

            if (parsedValue is null)
            {
                throw new InvalidOperationException();
            }

            return FilterExpressionBuilder.GreaterThan(property, parsedValue);
        }
    }
}
