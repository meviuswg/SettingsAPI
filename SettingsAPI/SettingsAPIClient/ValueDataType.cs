using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIClient
{
    public enum ValueDataType
    {
        Int,
        String,
        Decimal,
        ByteArray,
        DateTime,
        Bool,
        Json,
        Xml,
        Image,
        Custom = 0,
    }

    public class TypeNameResolver
    {
        public static string Resolve(ValueDataType dataType)
        {
            switch (dataType)
            {
                case ValueDataType.Int:
                    return typeof(int).ToString();
                case ValueDataType.Decimal:
                    return typeof(decimal).ToString();
                case ValueDataType.ByteArray:
                    return typeof(byte[]).ToString();
                case ValueDataType.DateTime:
                    return typeof(DateTime).ToString();
                case ValueDataType.Bool:
                    return typeof(bool).ToString();
                case ValueDataType.Image:
                    return typeof(Image).ToString();
                case ValueDataType.Json:
                    return "Json";
                case ValueDataType.Xml:
                    return "Xml";
                case ValueDataType.String:
                    return typeof(string).ToString();
                default:
                    return "Custom";
            }
        }

        public static ValueDataType Resolve(string dataTypeName)
        {
            if (string.Equals(typeof(int).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Int;

            if (string.Equals(typeof(Int32).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Int;

            if (string.Equals(typeof(Int16).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Int;

            if (string.Equals(typeof(decimal).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Decimal;

            if (string.Equals(typeof(byte[]).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.ByteArray; 

            if (string.Equals(typeof(DateTime).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.DateTime;
            
            if (string.Equals(typeof(bool).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Bool;

            if (string.Equals("json", dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Json;

            if (string.Equals("xml", dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Xml;

            if (string.Equals(typeof(Image).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.Image;

            if (string.Equals(typeof(string).ToString(), dataTypeName, StringComparison.CurrentCultureIgnoreCase))
                return ValueDataType.String;

            return ValueDataType.Custom;
        }
    }
}
