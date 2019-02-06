using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    public class Metadata
    {
        public IEnumerable<UIMessage> Errors { get; set; } = new List<UIMessage>();
        public IEnumerable<UIMessage> Infos { get; set; } = new List<UIMessage>();
        public IEnumerable<UIMessage> Warnings { get; set; } = new List<UIMessage>();
    }
}
