using System;

namespace Azure.Storage.Table
{
    internal sealed class EntityPropertyInfo
    {
        internal string PropertyName { get; }

        internal object Value { get; }

        internal string EdmDataType { get; }

        internal EntityPropertyInfo(string propertyName, object value, Type type)
        {
            Value = type == typeof(double) || type == typeof(long) ? value.ToString() : value;

            PropertyName = propertyName;

            if (type == typeof(DateTime)) EdmDataType = "Edm.DateTime";

            if (type == typeof(Guid)) EdmDataType = "Edm.Guid";

            if (type == typeof(long)) EdmDataType = "Edm.Int64";

            if (type == typeof(double)) EdmDataType = "Edm.Double";

            if (type == typeof(byte[])) EdmDataType = "Edm.Binary";
        }
    }
}