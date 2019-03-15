namespace Azure.TableStorage
{
    public sealed class TableQueryOptions
    {
        internal string Filter { get; set; }

        public int Top { get; set; }

        public string SelectProperties { get; set; }
    }
}
