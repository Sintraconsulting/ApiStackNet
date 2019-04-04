using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ApiStackNet.Core
{
    public static class ConversionHelper
    {

        public static object StringToObject(string value, Type propertyType)
        {
            object typedValue;

            if (value == null)
            {
                return null;
            }

            var extPropertyType = propertyType;

            if (propertyType.Name == "Nullable`1")
            {
                extPropertyType = propertyType.GenericTypeArguments[0];
            }

            switch (extPropertyType.FullName)
            {
                case "System.Boolean":
                case "bool":
                    typedValue = bool.Parse(value);
                    break;
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "int":
                    typedValue = int.Parse(value);
                    break;
                case "System.Guid":
                    typedValue = new Guid(value);
                    break;
                case "System.DateTime":
                    typedValue = DateTime.ParseExact(
                                value,
                                "s",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.AssumeUniversal);
                    break;
                default:
                    typedValue = value;
                    break;
            }

            return typedValue;
        }
    }
}
