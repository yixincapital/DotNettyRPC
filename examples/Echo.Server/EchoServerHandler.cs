﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace Echo.Server
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using rpc.model;
    using rpc.serialize;

    public class EchoServerHandler : ChannelHandlerAdapter
    {
        DefaultSerializer serializable;

        public EchoServerHandler()
        {
            this.serializable = new DefaultSerializer();
        }
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
          
            IByteBuffer buffer = message as SlicedByteBuffer;
            var response = new MessageResponse();
            if (buffer != null)
            {
                var request = (MessageRequest)this.serializable.Deserialize(buffer.ToArray());
                Console.WriteLine("Received from client: " + buffer.ToString(Encoding.UTF8));
                response.MessageId = request.MessageId;
                response.Result = "hello world";
            }

            context.WriteAsync(this.serializable.Serialize(message));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
          //  Console.WriteLine("Exception: " + exception);
           // context.CloseAsync();
        }

        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
    }
}
