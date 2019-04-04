using System;

namespace Azure.Storage.Table.PerformanceTest
{
    public class EdmEntity : TableEntity
    {
        public Guid GuidProp { get; set; }
        public DateTime DateProp { get; set; }
        public bool? Test { get; set; }
        public long? LongProperty { get; set; }
        public int? IntProperty { get; set; }
        public double? DoubleProperty { get; set; }
        public byte[] ByteTest { get; set; }

        public EdmEntity() : base("edmLocal")
        {

        }
    }
}
