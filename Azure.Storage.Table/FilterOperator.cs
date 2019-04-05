using System;

namespace Azure.Storage.Table
{
    public readonly struct FilterOperator : IEquatable<FilterOperator>
    {
        private static readonly FilterOperator _and = new FilterOperator(" and ");

        private static readonly FilterOperator _or = new FilterOperator(" or ");

        private static readonly FilterOperator _not = new FilterOperator(" not ");

        public readonly string Operator;

        private FilterOperator(string @operator) => Operator = @operator;

        public bool Equals(FilterOperator other) => other.Operator == Operator;

        public override bool Equals(object obj) => obj is FilterOperator l && Equals(l);

        public override int GetHashCode() => Operator.GetHashCode();

        public static FilterOperator And => _and;

        public static FilterOperator Or => _or;

        public static FilterOperator Not => _not;

        public static bool operator ==(FilterOperator x, FilterOperator y) => x.Operator == y.Operator && x.Operator == y.Operator;

        public static bool operator !=(FilterOperator x, FilterOperator y) => !(x == y);
    }
}