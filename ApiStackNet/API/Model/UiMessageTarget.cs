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
    public enum UiMessageTarget
    {
        [EnumMember(Value = "TOAST")]
        TOAST,
        [EnumMember(Value = "VALIDATION")]
        VALIDATION,
        [EnumMember(Value = "MODAL")]
        MODAL,
        [EnumMember(Value = "CONSOLE")]
        CONSOLE,
    }

}
