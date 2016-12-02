// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Echo.Client
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using rpc.model;
    using rpc.serialize;

    public class EchoClientHandler : ChannelHandlerAdapter
    {
        readonly IByteBuffer initialMessage;
        readonly byte[] buffer;
        readonly ISerializer serializer; 
        public EchoClientHandler()
        {
            this.buffer = new byte[EchoClientSettings.Size];
            this.initialMessage = Unpooled.Buffer(EchoClientSettings.Size);
            this.serializer=new DefaultSerializer();
            var request = new MessageRequest();
            request.MessageId = Guid.NewGuid().ToString();
            request.ClassName = "yixin.base.service.carservice";
            request.MethodName = "getlist";
            request.ParamTypes = new Type[2] { typeof(int), typeof(string) };
            request.Parameters = new object[] { 123, "onsale" };
         //   byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
            byte[] messageBytes = this.serializer.Serialize(request);
           this.initialMessage.WriteBytes(messageBytes);                    
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {          
            context.WriteAndFlushAsync(this.initialMessage);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {                
                Console.WriteLine("Received from server: " + byteBuffer.ToString(Encoding.UTF8));
            }
           context.WriteAsync(message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

    }
}
