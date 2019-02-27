using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UiMessageType
    {
        [EnumMember(Value = "SUCCESS")]
        SUCCESS,
        [EnumMember(Value = "INFO")]
        INFO,
        [EnumMember(Value = "WARNING")]
        WARNING,
        [EnumMember(Value = "ERROR")]
        ERROR,
    }

}
