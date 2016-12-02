﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace rpc.core
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using rpc.model;
    using rpc.serialize;

    public class MessageSendHandler : ChannelHandlerAdapter
    {
        readonly IByteBuffer requestMessage;
        readonly ConcurrentDictionary<string, MessageSendCallBack> callBackActions = new ConcurrentDictionary<string, MessageSendCallBack>();
        IChannelHandlerContext channelContext;      
        public MessageSendHandler()
        {
            this.requestMessage = Unpooled.Buffer(MessageSendSettings.Size);
        }
        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            base.ChannelRegistered(context);
            this.channelContext = context;       
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
         
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                var messageId = byteBuffer.ToString(Encoding.UTF8);
                MessageSendCallBack callBack = callBackActions[messageId];
                callBack.Over(new MessageResponse());
                Console.WriteLine("Received from server: " + messageId);

            }  
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }

        public MessageSendCallBack SendRequest(MessageRequest request)
        {
            var callBack = new MessageSendCallBack(request);
            this.callBackActions.TryAdd(request.MessageId, callBack);
            byte[] messageBytes = Encoding.UTF8.GetBytes(request.MessageId);
            this.requestMessage.WriteBytes(messageBytes);
            this.channelContext.WriteAndFlushAsync(this.requestMessage);
            return callBack;
        }
    }
}