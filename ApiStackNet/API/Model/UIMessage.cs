using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    public class UiMessage
    {
        public string Code { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public UiMessageType Type { get; set; }

        public string FieldId { get; set; }

        public UiMessageTarget Target { get; set; }

        public string CultureName { get; set; }


        public UiMessage() { }



    }
}
