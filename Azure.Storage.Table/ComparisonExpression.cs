using System;
using System.Globalization;

namespace Azure.Storage.Table
{
    public sealed class CompareCondition
    {
        internal readonly string FilterString;

        public CompareCondition(string propertyName, CompareOperator opertaor, string value)
            : this(propertyName, opertaor, value, TableDataType.String) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, bool value)
            : this(propertyName, opertaor, value ? "true" : "false", TableDataType.Boolean) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, DateTimeOffset value)
            : this(propertyName, opertaor, value.UtcDateTime.ToString("o"), TableDataType.DateTime) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, double value)
            : this(propertyName, opertaor, value.ToString(CultureInfo.InvariantCulture), TableDataType.Double) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, int value)
            : this(propertyName, opertaor, value.ToString(CultureInfo.InvariantCulture), TableDataType.Int32) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, long value)
            : this(propertyName, opertaor, value.ToString(CultureInfo.InvariantCulture) + "L", TableDataType.Int64) { }

        public CompareCondition(string propertyName, CompareOperator opertaor, Guid value)
            : this(propertyName, opertaor, value.ToString(), TableDataType.Guid) { }

        private CompareCondition(string propertyName, CompareOperator opertaor, string value, TableDataType dataType)
        {
            string valueString;

            switch (dataType)
            {
                case TableDataType.Boolean:
                case TableDataType.Int32:
                case TableDataType.Int64:
                    valueString = value;
                    break;
                case TableDataType.DateTime:
                    valueString = "datetime'" + value + "'";
                    break;
                case TableDataType.Double:
                    valueString = int.TryParse(value, out var parsedValue) ? parsedValue.ToString(CultureInfo.InvariantCulture) + ".0" : value;
                    break;
                case TableDataType.Guid:
                    valueString = "guid'" + value + "'";
                    break;
                default:
                    if (value.IndexOf('\'') != -1) value = value.Replace("'", "''");
                    valueString = "'" + value + "'";
                    break;
            }

            FilterString = "(" + propertyName + opertaor.Operator + valueString + ")";
        }

        private enum TableDataType
        {
            String,
            Boolean,
            DateTime,
            Double,
            Guid,
            Int32,
            Int64
        }
    }
}