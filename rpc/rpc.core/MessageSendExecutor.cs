// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Threading;
    using NProxy.Core;
    using rpc.serialize;

    public class MessageSendExecutor
    {
        RpcServerLoader loader = RpcServerLoader.Instance;
        private static ProxyFactory _proxyFactory;
      
        public MessageSendExecutor()
        {
         
        }

        public MessageSendExecutor(string serverAddr, int port, ISerializer serializer)
        {
            this.loader.Load(serverAddr,port,serializer);
         
        }

        public void SetRpcServerLoader(string serverAddr, int port, ISerializer serializer) 
        {
            this.loader.Load(serverAddr, port, serializer);
       
        }

        public  T Execute<T>() where T : class
        {
            _proxyFactory = new ProxyFactory();
            var proxy = _proxyFactory.CreateProxy<T>(Type.EmptyTypes, new MessageSendProxy());
            return proxy;
        }

        public void Stop()
        {
            //TODO stop current executor
        }
    }
}