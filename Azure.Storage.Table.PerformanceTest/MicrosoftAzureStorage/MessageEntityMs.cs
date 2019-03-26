using System;

namespace Azure.Storage.Table.PerformanceTest.MicrosoftAzureStorage
{
    public sealed class MessageEntityMs : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public string Message { get; set; }
    }

    public class DemoMs : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public Guid GuidProp { get; set; }
        public DateTime DateProp { get; set; }
        public bool? Test { get; set; }
        public long? LongProperty { get; set; }
        public int? IntProperty { get; set; }
        public double? DoubleProperty { get; set; }
        public byte[] ByteTest { get; set; }
    }
}