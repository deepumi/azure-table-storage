namespace Azure.Storage.Table
{
    public sealed class FilterCondition
    {
        public FilterOperator FilterOperator { get; set; }

        public CompareCondition[] Conditions { get; set; }

        public FilterCondition(FilterOperator filterOperator)
        {
            FilterOperator = filterOperator;
        }

        public FilterCondition() { }
    }
}