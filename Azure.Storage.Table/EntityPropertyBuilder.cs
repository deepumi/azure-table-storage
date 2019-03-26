using System;
using System.Collections.Generic;
using System.Reflection;

namespace Azure.Storage.Table
{
    internal static class EntityPropertyBuilder
    {
        internal static IDictionary<string, object> Build(ITableEntity entity)
        {
            var entityProperties = GetEntityProperites(entity);

            var result = new Dictionary<string, object>();

            for(var i =0; i < entityProperties.Count; i++)
            {
                result.Add(entityProperties[i].PropertyName, entityProperties[i].Value);

                if (!string.IsNullOrEmpty(entityProperties[i].EdmDataType))
                    result.Add(entityProperties[i].PropertyName + "@odata.type", entityProperties[i].EdmDataType);
            }

            return result;
        }

        private static List<EntityPropertyInfo> GetEntityProperites(ITableEntity entity)
        {
            var properties = entity.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<EntityPropertyInfo>(properties.Length);

            for (var i = 0; i < properties.Length; i++)
            {
                if (string.Compare(properties[i].Name, "TableName", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(properties[i].Name, "Timestamp", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(properties[i].Name, "ETag", StringComparison.OrdinalIgnoreCase) == 0) continue;

                list.Add(new EntityPropertyInfo(properties[i].Name, properties[i].GetValue(entity), properties[i].PropertyType));
            }

            return list;
        }
    }
}