using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpc.model
{
    [Serializable]
    public class MessageRequest
    {
        public string MessageId { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public Type[] ParamTypes { get; set; } 

        public object[] Parameters { get; set; }
    }
}
