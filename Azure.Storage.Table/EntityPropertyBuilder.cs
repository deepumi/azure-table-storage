using System;
using System.Collections.Generic;
using System.Reflection;

namespace Azure.Storage.Table
{
    internal static class EntityPropertyBuilder
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;

        internal static IDictionary<string, object> Build(ITableEntity entity)
        {
            var properties = entity.GetType().GetProperties(Flags);

            var result = new Dictionary<string, object>();

            for (var i = 0; i < properties.Length; i++)
            {
                if (string.Compare(properties[i].Name, "TableName", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(properties[i].Name, "Timestamp", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(properties[i].Name, "ETag", StringComparison.OrdinalIgnoreCase) == 0) continue;

                var entityInfo = new EntityPropertyInfo(properties[i].GetValue(entity), properties[i].PropertyType);

                result.Add(properties[i].Name, entityInfo.Value);

                if (entityInfo.EdmDataType != null)
                    result.Add(properties[i].Name + "@odata.type", entityInfo.EdmDataType);
            }

            return result;
        }
    }
}