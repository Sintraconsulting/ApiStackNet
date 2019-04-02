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

        public static object StringToObject(string value, Type extPropertyType)
        {
            object typedValue;

            if (value == null)
            {
                return null;
            }

            if (extPropertyType.Name == "Nullable`1")
            {

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
                    if (extPropertyType.FullName.StartsWith("System.Nullable`1[[System.Int32"))
                    {
                        typedValue = int.Parse(value);
                    }
                    else
                    {
                        typedValue = value;
                    }
                    break;
            }

            return typedValue;
        }
    }
}
