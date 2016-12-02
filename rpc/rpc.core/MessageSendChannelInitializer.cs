// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Diagnostics.Contracts;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using rpc.serialize;

    public class MessageSendChannelInitializer<T> : ChannelInitializer<T>
        where T : IChannel
    {
        readonly Action<T> initializationAction;
        ISerializer protocol;
        private ISerializeFrame frame =new MessageSendSerializeFrame();

        public MessageSendChannelInitializer(Action<T> initializationAction)
        {
         
            this.initializationAction = initializationAction;
        }
        public MessageSendChannelInitializer<T> BuildSerializeProtocol(ISerializer protocol)
        {
            this.protocol = protocol;
            return this;
        } 
        protected override void InitChannel(T channel)
        {
            this.initializationAction(channel);
            IChannelPipeline pipeline = channel.Pipeline;
            this.frame.Select(this.protocol,pipeline);
        }
    }
}