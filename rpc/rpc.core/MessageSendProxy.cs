using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpc.core
{
    using System.Reflection;
    using NProxy.Core;
    using rpc.model;

    public class MessageSendProxy:IInvocationHandler
    {
        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo is null");
            }
            // get class name 
            // ReSharper disable once PossibleNullReferenceException
            string className = methodInfo.DeclaringType.FullName;
            ParameterInfo[] paramWrapper = methodInfo.GetParameters();
          
            var request = new MessageRequest();
            request.MessageId = Guid.NewGuid().ToString();
            request.ClassName = methodInfo.DeclaringType.FullName;
            request.MethodName = methodInfo.Name;
            request.ParamTypes = paramWrapper.Select(s => s.ParameterType).ToArray();
            request.Parameters = parameters;

            MessageSendHandler handler = RpcServerLoader.Instance.GetMessageSendHandler();
            MessageSendCallBack callBack = handler.SendRequest(request);
            return callBack.Start();
        }
    }
}
