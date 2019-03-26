namespace Azure.Storage.Table
{
    internal enum TableOperationType : byte
    {
        Get = 1,
        Insert = 2,
        InsertEdmType = 3,
        Update = 4,
        Delete = 5
    }
}