using System;

namespace Azure.Storage.Table
{
    public readonly struct CompareOperator : IEquatable<CompareOperator>
    {
        private static readonly CompareOperator _equal = new CompareOperator(" eq ");

        private static readonly CompareOperator _greaterThan = new CompareOperator(" gt ");

        private static readonly CompareOperator _greaterThanEqual = new CompareOperator(" ge ");

        private static readonly CompareOperator _lessThan = new CompareOperator(" lt ");

        private static readonly CompareOperator _lessThanEqual = new CompareOperator(" le ");

        private static readonly CompareOperator _notEqual = new CompareOperator(" ne ");

        internal readonly string Operator;

        public CompareOperator(string @operator) => Operator = @operator;

        public bool Equals(CompareOperator other) => other.Operator == Operator;

        public override bool Equals(object obj) => obj is CompareOperator co && Equals(co);

        public override int GetHashCode() => Operator.GetHashCode();

        public static CompareOperator Equal => _equal;

        public static CompareOperator GreaterThan => _greaterThan;

        public static CompareOperator GreaterThanOrEqual => _greaterThanEqual;

        public static CompareOperator LessThan => _lessThan;

        public static CompareOperator LessThanOrEqual => _lessThanEqual;

        public static CompareOperator NotEqual => _notEqual;

        public static bool operator ==(CompareOperator x, CompareOperator y) => x.Operator == y.Operator && x.Operator == y.Operator;

        public static bool operator !=(CompareOperator x, CompareOperator y) => !(x == y);
    }
}