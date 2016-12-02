// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Net;
    using DotNetty.Transport.Channels;
    using rpc.serialize;

    public class RpcServerLoader
    {
        private  static readonly Lazy<RpcServerLoader> lazy = new Lazy<RpcServerLoader>(()=>new RpcServerLoader());
        private MessageSendHandler messageSendHandler = null;
        private readonly IEventLoopGroup eventLoopGroup = new MultithreadEventLoopGroup();
         public static RpcServerLoader Instance { get { return lazy.Value; } }

        private RpcServerLoader()
        {
            
        }

        public async void Load(string serverAddr, int port, ISerializer serializer)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverAddr), port);
            var connector = new MessageSendConnector(this.eventLoopGroup,ipEndPoint,serializer);
            
            await connector.Call(); // TODO  asynchronously run
        }

        public void SetMessageSendHandler(MessageSendHandler handler)
        {
            this.messageSendHandler = handler;  //TODO add thread safe lock 
        }

        public MessageSendHandler GetMessageSendHandler()
        {
            return this.messageSendHandler;
        }
    }
}